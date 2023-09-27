#if !MORPEH_DEBUG
#define MORPEH_DEBUG_DISABLED
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Events;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EventListener<T> : EventListener where T : IWorldEvent
    {
        private FastList<T> scheduledEventsForNextFrame = new();
        private FastList<T> publishedEventsForThisFrame = new();
        
        private bool hasScheduledEventsForNextFrame;
        private event Action<FastList<T>> callbacks;

        public bool HasPublishedEventsThisFrame { get; private set; }
        private bool IsUpdateScheduled => HasPublishedEventsThisFrame || hasScheduledEventsForNextFrame;

        public EventListener(EventsFeature feature) : base(feature)
        {
        }

        [PublicAPI]
        internal void ScheduleEventForNextFrame(T data)
        {
            scheduledEventsForNextFrame.Add(data);

            if (!IsUpdateScheduled)
                feature.eventListenersRequireUpdate.Add(this);

            hasScheduledEventsForNextFrame = true;
        }

        public FastList<T>.Enumerator GetEnumerator()
        {
            return publishedEventsForThisFrame.GetEnumerator();
        }

        [PublicAPI]
        internal IDisposable Subscribe(Action<FastList<T>> callback)
        {
            return new Subscription(this, callback);
        }

        private class Subscription : IDisposable
        {
            private readonly EventListener<T> _owner;
            private readonly Action<FastList<T>> _callback;

            public Subscription(EventListener<T> owner, Action<FastList<T>> callback)
            {
                _owner = owner;
                _callback = callback;

                _owner.callbacks += _callback;
            }

            public void Dispose()
            {
                _owner.callbacks -= _callback;
            }
        }

        /// <summary>
        /// Returns true if update is needed in the next frame
        /// </summary>
        internal sealed override void Update()
        {
            if (HasPublishedEventsThisFrame)
            {
                if (callbacks != null)
                {
                    TryCatchInvokeCallback();
                    ForwardInvokeCallback();
                }

                HasPublishedEventsThisFrame = false;
                publishedEventsForThisFrame.Clear();
            }

            if (!hasScheduledEventsForNextFrame)
                return;

            HasPublishedEventsThisFrame = true;
            hasScheduledEventsForNextFrame = false;

            (publishedEventsForThisFrame, scheduledEventsForNextFrame) = (scheduledEventsForNextFrame, publishedEventsForThisFrame);
            scheduledEventsForNextFrame.Clear();

            feature.eventListenersRequireUpdate.Add(this);
        }

        [Conditional("MORPEH_DEBUG_DISABLED")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ForwardInvokeCallback()
        {
            callbacks?.Invoke(publishedEventsForThisFrame);
        }

        [Conditional("MORPEH_DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TryCatchInvokeCallback()
        {
            try
            {
                callbacks?.Invoke(publishedEventsForThisFrame);
            }
            catch (Exception ex)
            {
                MLogger.LogError($"Can not invoke callback Event<{typeof(T)}>");
                MLogger.LogException(ex);
            }
        }
    }

    public abstract class EventListener
    {
        internal EventsFeature feature;

        protected EventListener(EventsFeature feature)
        {
            this.feature = feature;
        }

        internal abstract void Update();
    }
}