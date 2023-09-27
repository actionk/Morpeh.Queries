using System;
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
        public static void ScheduleEventForEntity<T>(this IQuerySystem querySystem, Entity entity) where T : IWorldEvent, new()
        {
            if (!querySystem.IsUpdatedEveryFrame)
                throw new Exception($"You should never subscribe to events in a QuerySystem [{querySystem.GetType().Name}] that doesn't update each frame, this will lead to losing events!");

            ScheduleEventForEntity(querySystem.World, entity, new T());
        }

        [PublicAPI]
        public static void ScheduleEventForEntity<T>(this IQuerySystem querySystem, Entity entity, T data) where T : IWorldEvent
        {
            if (!querySystem.IsUpdatedEveryFrame)
                throw new Exception($"You should never subscribe to events in a QuerySystem [{querySystem.GetType().Name}] that doesn't update each frame, this will lead to losing events!");

            ScheduleEventForEntity(querySystem.World, entity, data);
        }

        [PublicAPI]
        public static void ScheduleEventForEntity<T>(this Entity entity) where T : IWorldEvent, new()
        {
            ScheduleEventForEntity(entity.world, entity, new T());
        }

        [PublicAPI]
        public static void ScheduleEventForEntity<T>(this World world, Entity entity) where T : IWorldEvent, new()
        {
            ScheduleEventForEntity(world, entity, new T());
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
        public static CompiledEventListener<T> CreateEventListener<T>(this IQuerySystem querySystem) where T : IWorldEvent
        {
            var type = typeof(T);
            if (!querySystem.World.TryGetFeature(out EventsFeature eventFeature))
            {
                Debug.LogError($"You should enable [{nameof(EventsFeature)}] for world [{querySystem.World}] before using [{nameof(CreateEventListener)}]!");
                return default;
            }

            if (eventFeature.eventListenersByEventType.TryGetValue(type, out var registeredEvent))
                return new CompiledEventListener<T>(querySystem, (EventListener<T>)registeredEvent);

            registeredEvent = new EventListener<T>(eventFeature);
            eventFeature.eventListenersByEventType.Add(type, registeredEvent);
            return new CompiledEventListener<T>(querySystem, (EventListener<T>)registeredEvent);
        }

        [PublicAPI]
        public static CompiledEventListener<EventWithEntity<T>> CreateEntityEventListener<T>(this IQuerySystem querySystem) where T : IWorldEvent
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