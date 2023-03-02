using System;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Events;

namespace Scellecs.Morpeh
{
    public static class EventListenerExtensions
    {
#region ForAll

        public static EventListener<T> ForAll<T>(this EventListener<T> eventListener, Action<FastList<T>> callback) where T : IWorldEvent
        {
            eventListener.Subscribe(callback);
            return eventListener;
        }

#endregion
        
#region ForEach
        
        // ------------------------------------------------- //
        // 0 parameters
        // ------------------------------------------------- //
        
        public delegate void ForEachDelegate<in T>(T eventData) where T : IWorldEvent;

        public static EventListener<T> ForEach<T>(this EventListener<T> eventListener, ForEachDelegate<T> callback) where T : IWorldEvent
        {
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;
                
                foreach(var eventData in eventListener)
                    callback.Invoke(eventData);
            });
            return eventListener;
        }
        
        public delegate void ForEachDelegateWithEntity<in T>(Entity entity, T eventData) where T : IWorldEvent;

        public static EventListener<EventWithEntity<T>> ForEach<T>(this EventListener<EventWithEntity<T>> eventListener, ForEachDelegateWithEntity<T> callback) where T : IWorldEvent
        {
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;
                
                foreach(var eventData in eventListener)
                    callback.Invoke(eventData.entity, eventData.eventData);
            });
            return eventListener;
        }
        
        public delegate void ForEachDelegateWithEntityWithoutEvent(Entity entity);

        public static EventListener<EventWithEntity<T>> ForEach<T>(this EventListener<EventWithEntity<T>> eventListener, ForEachDelegateWithEntityWithoutEvent callback) where T : IWorldEvent
        {
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;
                
                foreach(var eventData in eventListener)
                    callback.Invoke(eventData.entity);
            });
            return eventListener;
        }
        
        // ------------------------------------------------- //
        // 1 parameter
        // ------------------------------------------------- //
        
        public delegate void ForEachDelegateWithEntityP1<TP1>(Entity entity, ref TP1 component1) where TP1: struct, IComponent;

        public static EventListener<EventWithEntity<T>> ForEach<T, TP1>(this EventListener<EventWithEntity<T>> eventListener, ForEachDelegateWithEntityP1<TP1> callback) 
            where T : IWorldEvent
            where TP1: struct, IComponent
        {
            var stash = eventListener.querySystem.World.GetStash<TP1>();
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stash.Get(eventData.entity, out var tp1Exists);
                    if (!tp1Exists)
                        continue;
                    
                    callback.Invoke(eventData.entity, ref component1);
                }
            });
            return eventListener;
        }
        
#endregion
    }
}