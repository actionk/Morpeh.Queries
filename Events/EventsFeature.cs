using System;
using System.Collections.Generic;
using Scellecs.Morpeh.Collections;
using UnityEngine.Scripting;

namespace Scellecs.Morpeh
{
    [Preserve]
    public class EventsFeature : IWorldFeature
    {
        internal readonly Dictionary<Type, EventListener> eventListenersByEventType = new();
        internal FastList<EventListener> eventListenersRequireUpdate = new();
        internal FastList<EventListener> eventListenersBeingUpdated = new();

        [Preserve]
        public EventsFeature()
        {
        }

        [Preserve]
        public void Initialize(World world)
        {
        }

        public void OnCleanupUpdate()
        {
            var list = eventListenersRequireUpdate;
            if (list.length == 0)
                return;

            eventListenersRequireUpdate = eventListenersBeingUpdated;
            eventListenersBeingUpdated = list;
            
            foreach (var eventListenerToUpdate in list)
                eventListenerToUpdate.Update();
            
            list.Clear();
        }

        public void Dispose()
        {
        }
    }
}