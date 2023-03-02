using JetBrains.Annotations;
using Plugins.morpeh_plugins.Morpeh.WorldFeatures;
using Scellecs.Morpeh.Events;
using UnityEngine;

namespace Scellecs.Morpeh
{
    public static class EventSystemExtensions
    {
        // ------------------------------------------------- //
        // Scheduling Events
        // ------------------------------------------------- //

        [PublicAPI]
        public static void ScheduleEventForEntity<T>(this IQuerySystem querySystem, Entity entity, T data) where T : IWorldEvent
        {
            ScheduleEventForEntity(querySystem.World, entity, data);
        }

        [PublicAPI]
        public static void ScheduleEventForEntity<T>(this World world, Entity entity, T data) where T : IWorldEvent
        {
            // if there are no listeners - it doesn't make sense to schedule an event
            if (!world.TryGetEventListener(out EventListener<EventWithEntity<T>> eventListener))
                return;
            
            var eventData = new EventWithEntity<T>
            {
                entity = entity,
                eventData = data
            };
            eventListener.ScheduleEventForNextFrame(eventData);
        }

        [PublicAPI]
        public static void ScheduleEvent<T>(this IQuerySystem querySystem, T data) where T : IWorldEvent
        {
            ScheduleEvent(querySystem.World, data);
        }

        [PublicAPI]
        public static void ScheduleEvent<T>(this World world, T data) where T : IWorldEvent
        {
            // if there are no listeners - it doesn't make sense to schedule an event
            if (!world.TryGetEventListener(out EventListener<T> eventListener))
                return;
            
            eventListener.ScheduleEventForNextFrame(data);
        }

        // ------------------------------------------------- //
        // Creating Listeners
        // ------------------------------------------------- //

        [PublicAPI]
        public static EventListener<T> CreateEventListener<T>(this IQuerySystem querySystem) where T : IWorldEvent
        {
            var type = typeof(T);
            if (!querySystem.World.TryGetFeature(out EventsFeature eventFeature))
            {
                Debug.LogError($"You should enable [{nameof(EventsFeature)}] for world [{querySystem.World}] before using [{nameof(CreateEventListener)}]!");
                return default;
            }

            if (eventFeature.eventListenersByEventType.TryGetValue(type, out var registeredEvent))
                return (EventListener<T>)registeredEvent;

            registeredEvent = new EventListener<T>(querySystem, eventFeature);
            eventFeature.eventListenersByEventType.Add(type, registeredEvent);
            return (EventListener<T>)registeredEvent;
        }

        [PublicAPI]
        public static EventListener<EventWithEntity<T>> CreateEntityEventListener<T>(this IQuerySystem querySystem) where T : IWorldEvent
        {
            return CreateEventListener<EventWithEntity<T>>(querySystem);
        }

        [PublicAPI]
        private static bool TryGetEventListener<T>(this World world, out EventListener<T> eventListener) where T : IWorldEvent
        {
            var type = typeof(T);
            if (!world.TryGetFeature(out EventsFeature eventFeature))
            {
                eventListener = default;
                Debug.LogError($"You should enable [{nameof(EventsFeature)}] for world [{world}] before using [{nameof(TryGetEventListener)}]!");
                return false;
            }

            if (eventFeature.eventListenersByEventType.TryGetValue(type, out var registeredEvent))
            {
                eventListener = (EventListener<T>)registeredEvent;
                return true;
            }

            eventListener = default;
            return false;
        }
    }
}