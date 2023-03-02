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
        
        public static EventListener<EventWithEntity<T>> ForEach<T, TP1>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EVC<T, TP1> callback) 
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
                    
                    callback.Invoke(eventData.entity, eventData.eventData, ref component1);
                }
            });
            return eventListener;
        }
        
        public static EventListener<EventWithEntity<T>> ForEach<T, TP1>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EC<TP1> callback) 
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
        
        // ------------------------------------------------- //
        // 2 parameters
        // ------------------------------------------------- //
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EVC<T, T1, T2> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    callback.Invoke(eventData.entity, eventData.eventData, ref component1, ref component2);
                }
            });
            return eventListener;
        }
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EC<T1, T2> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    callback.Invoke(eventData.entity, ref component1, ref component2);
                }
            });
            return eventListener;
        }
        
        // ------------------------------------------------- //
        // 3 parameters
        // ------------------------------------------------- //
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EVC<T, T1, T2, T3> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    callback.Invoke(eventData.entity, eventData.eventData, ref component1, ref component2, ref component3);
                }
            });
            return eventListener;
        }
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EC<T1, T2, T3> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    callback.Invoke(eventData.entity, ref component1, ref component2, ref component3);
                }
            });
            return eventListener;
        }
        
        // ------------------------------------------------- //
        // 4 parameters
        // ------------------------------------------------- //
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3, T4>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EVC<T, T1, T2, T3, T4> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
            where T4: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            var stashT4 = eventListener.querySystem.World.GetStash<T4>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    ref var component4 = ref stashT4.Get(eventData.entity, out var exists4);
                    if (!exists4)
                        continue;
                    
                    callback.Invoke(eventData.entity, eventData.eventData, ref component1, ref component2, ref component3, ref component4);
                }
            });
            return eventListener;
        }
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3, T4>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EC<T1, T2, T3, T4> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
            where T4: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            var stashT4 = eventListener.querySystem.World.GetStash<T4>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    ref var component4 = ref stashT4.Get(eventData.entity, out var exists4);
                    if (!exists4)
                        continue;
                    
                    callback.Invoke(eventData.entity, ref component1, ref component2, ref component3, ref component4);
                }
            });
            return eventListener;
        }
        
        // ------------------------------------------------- //
        // 5 parameters
        // ------------------------------------------------- //
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3, T4, T5>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EVC<T, T1, T2, T3, T4, T5> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
            where T4: struct, IComponent
            where T5: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            var stashT4 = eventListener.querySystem.World.GetStash<T4>();
            var stashT5 = eventListener.querySystem.World.GetStash<T5>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    ref var component4 = ref stashT4.Get(eventData.entity, out var exists4);
                    if (!exists4)
                        continue;
                    
                    ref var component5 = ref stashT5.Get(eventData.entity, out var exists5);
                    if (!exists5)
                        continue;
                    
                    callback.Invoke(eventData.entity, eventData.eventData, ref component1, ref component2, ref component3, ref component4, ref component5);
                }
            });
            return eventListener;
        }
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3, T4, T5>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EC<T1, T2, T3, T4, T5> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
            where T4: struct, IComponent
            where T5: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            var stashT4 = eventListener.querySystem.World.GetStash<T4>();
            var stashT5 = eventListener.querySystem.World.GetStash<T5>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    ref var component4 = ref stashT4.Get(eventData.entity, out var exists4);
                    if (!exists4)
                        continue;
                    
                    ref var component5 = ref stashT5.Get(eventData.entity, out var exists5);
                    if (!exists5)
                        continue;
                    
                    callback.Invoke(eventData.entity, ref component1, ref component2, ref component3, ref component4, ref component5);
                }
            });
            return eventListener;
        }
        
        // ------------------------------------------------- //
        // 6 parameters
        // ------------------------------------------------- //
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3, T4, T5, T6>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EVC<T, T1, T2, T3, T4, T5, T6> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
            where T4: struct, IComponent
            where T5: struct, IComponent
            where T6: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            var stashT4 = eventListener.querySystem.World.GetStash<T4>();
            var stashT5 = eventListener.querySystem.World.GetStash<T5>();
            var stashT6 = eventListener.querySystem.World.GetStash<T6>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    ref var component4 = ref stashT4.Get(eventData.entity, out var exists4);
                    if (!exists4)
                        continue;
                    
                    ref var component5 = ref stashT5.Get(eventData.entity, out var exists5);
                    if (!exists5)
                        continue;
                    
                    ref var component6 = ref stashT6.Get(eventData.entity, out var exists6);
                    if (!exists6)
                        continue;
                    
                    callback.Invoke(eventData.entity, eventData.eventData, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6);
                }
            });
            return eventListener;
        }
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3, T4, T5, T6>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EC<T1, T2, T3, T4, T5, T6> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
            where T4: struct, IComponent
            where T5: struct, IComponent
            where T6: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            var stashT4 = eventListener.querySystem.World.GetStash<T4>();
            var stashT5 = eventListener.querySystem.World.GetStash<T5>();
            var stashT6 = eventListener.querySystem.World.GetStash<T6>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    ref var component4 = ref stashT4.Get(eventData.entity, out var exists4);
                    if (!exists4)
                        continue;
                    
                    ref var component5 = ref stashT5.Get(eventData.entity, out var exists5);
                    if (!exists5)
                        continue;
                    
                    ref var component6 = ref stashT6.Get(eventData.entity, out var exists6);
                    if (!exists6)
                        continue;
                    
                    callback.Invoke(eventData.entity, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6);
                }
            });
            return eventListener;
        }
        
        // ------------------------------------------------- //
        // 7 parameters
        // ------------------------------------------------- //
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3, T4, T5, T6, T7>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EVC<T, T1, T2, T3, T4, T5, T6, T7> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
            where T4: struct, IComponent
            where T5: struct, IComponent
            where T6: struct, IComponent
            where T7: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            var stashT4 = eventListener.querySystem.World.GetStash<T4>();
            var stashT5 = eventListener.querySystem.World.GetStash<T5>();
            var stashT6 = eventListener.querySystem.World.GetStash<T6>();
            var stashT7 = eventListener.querySystem.World.GetStash<T7>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    ref var component4 = ref stashT4.Get(eventData.entity, out var exists4);
                    if (!exists4)
                        continue;
                    
                    ref var component5 = ref stashT5.Get(eventData.entity, out var exists5);
                    if (!exists5)
                        continue;
                    
                    ref var component6 = ref stashT6.Get(eventData.entity, out var exists6);
                    if (!exists6)
                        continue;
                    
                    ref var component7 = ref stashT7.Get(eventData.entity, out var exists7);
                    if (!exists7)
                        continue;
                    
                    callback.Invoke(eventData.entity, eventData.eventData, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7);
                }
            });
            return eventListener;
        }
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3, T4, T5, T6, T7>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EC<T1, T2, T3, T4, T5, T6, T7> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
            where T4: struct, IComponent
            where T5: struct, IComponent
            where T6: struct, IComponent
            where T7: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            var stashT4 = eventListener.querySystem.World.GetStash<T4>();
            var stashT5 = eventListener.querySystem.World.GetStash<T5>();
            var stashT6 = eventListener.querySystem.World.GetStash<T6>();
            var stashT7 = eventListener.querySystem.World.GetStash<T7>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    ref var component4 = ref stashT4.Get(eventData.entity, out var exists4);
                    if (!exists4)
                        continue;
                    
                    ref var component5 = ref stashT5.Get(eventData.entity, out var exists5);
                    if (!exists5)
                        continue;
                    
                    ref var component6 = ref stashT6.Get(eventData.entity, out var exists6);
                    if (!exists6)
                        continue;
                    
                    ref var component7 = ref stashT7.Get(eventData.entity, out var exists7);
                    if (!exists7)
                        continue;
                    
                    callback.Invoke(eventData.entity, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7);
                }
            });
            return eventListener;
        }
        
        // ------------------------------------------------- //
        // 8 parameters
        // ------------------------------------------------- //
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3, T4, T5, T6, T7, T8>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EVC<T, T1, T2, T3, T4, T5, T6, T7, T8> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
            where T4: struct, IComponent
            where T5: struct, IComponent
            where T6: struct, IComponent
            where T7: struct, IComponent
            where T8: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            var stashT4 = eventListener.querySystem.World.GetStash<T4>();
            var stashT5 = eventListener.querySystem.World.GetStash<T5>();
            var stashT6 = eventListener.querySystem.World.GetStash<T6>();
            var stashT7 = eventListener.querySystem.World.GetStash<T7>();
            var stashT8 = eventListener.querySystem.World.GetStash<T8>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    ref var component4 = ref stashT4.Get(eventData.entity, out var exists4);
                    if (!exists4)
                        continue;
                    
                    ref var component5 = ref stashT5.Get(eventData.entity, out var exists5);
                    if (!exists5)
                        continue;
                    
                    ref var component6 = ref stashT6.Get(eventData.entity, out var exists6);
                    if (!exists6)
                        continue;
                    
                    ref var component7 = ref stashT7.Get(eventData.entity, out var exists7);
                    if (!exists7)
                        continue;
                    
                    ref var component8 = ref stashT8.Get(eventData.entity, out var exists8);
                    if (!exists8)
                        continue;
                    
                    callback.Invoke(eventData.entity, eventData.eventData, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8);
                }
            });
            return eventListener;
        }
        
        public static EventListener<EventWithEntity<T>> ForEach<T, T1, T2, T3, T4, T5, T6, T7, T8>(this EventListener<EventWithEntity<T>> eventListener, Callbacks.EC<T1, T2, T3, T4, T5, T6, T7, T8> callback) 
            where T : IWorldEvent
            where T1: struct, IComponent
            where T2: struct, IComponent
            where T3: struct, IComponent
            where T4: struct, IComponent
            where T5: struct, IComponent
            where T6: struct, IComponent
            where T7: struct, IComponent
            where T8: struct, IComponent
        {
            var stashT1 = eventListener.querySystem.World.GetStash<T1>();
            var stashT2 = eventListener.querySystem.World.GetStash<T2>();
            var stashT3 = eventListener.querySystem.World.GetStash<T3>();
            var stashT4 = eventListener.querySystem.World.GetStash<T4>();
            var stashT5 = eventListener.querySystem.World.GetStash<T5>();
            var stashT6 = eventListener.querySystem.World.GetStash<T6>();
            var stashT7 = eventListener.querySystem.World.GetStash<T7>();
            var stashT8 = eventListener.querySystem.World.GetStash<T8>();
            
            eventListener.querySystem.AddExecutor(() =>
            {
                if (!eventListener.HasPublishedEventsThisFrame)
                    return;

                foreach (var eventData in eventListener)
                {
                    ref var component1 = ref stashT1.Get(eventData.entity, out var exists1);
                    if (!exists1)
                        continue;
                    
                    ref var component2 = ref stashT2.Get(eventData.entity, out var exists2);
                    if (!exists2)
                        continue;
                    
                    ref var component3 = ref stashT3.Get(eventData.entity, out var exists3);
                    if (!exists3)
                        continue;
                    
                    ref var component4 = ref stashT4.Get(eventData.entity, out var exists4);
                    if (!exists4)
                        continue;
                    
                    ref var component5 = ref stashT5.Get(eventData.entity, out var exists5);
                    if (!exists5)
                        continue;
                    
                    ref var component6 = ref stashT6.Get(eventData.entity, out var exists6);
                    if (!exists6)
                        continue;
                    
                    ref var component7 = ref stashT7.Get(eventData.entity, out var exists7);
                    if (!exists7)
                        continue;
                    
                    ref var component8 = ref stashT8.Get(eventData.entity, out var exists8);
                    if (!exists8)
                        continue;
                    
                    callback.Invoke(eventData.entity, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8);
                }
            });
            return eventListener;
        }
        
#endregion
    }
}