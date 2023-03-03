using Scellecs.Morpeh.Events;

namespace Scellecs.Morpeh
{
    public readonly struct CompiledEventListener<T> where T: IWorldEvent
    {
        public readonly IQuerySystem querySystem;
        public readonly EventListener<T> listener;

        public CompiledEventListener(IQuerySystem querySystem, EventListener<T> listener)
        {
            this.querySystem = querySystem;
            this.listener = listener;
        }
    }
}