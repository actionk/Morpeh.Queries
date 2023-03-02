using Scellecs.Morpeh.Events;

namespace Scellecs.Morpeh
{
    public struct EventWithEntity<T> : IWorldEvent where T : IWorldEvent
    {
        public Entity entity;
        public T eventData;
    }
}