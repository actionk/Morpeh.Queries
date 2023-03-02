using System.Collections;
using System.Collections.Generic;
using Scellecs.Morpeh.Native;

namespace Scellecs.Morpeh
{
    public readonly struct CompiledQuery : IEnumerable<Entity>
    {
        public readonly Filter filter;
        public readonly bool hasFilter;
        public World world => filter.world;

        private struct WorldEntitiesEnumerator : IEnumerator<Entity>
        {
            public readonly Entity[] entities;
            private int index;

            public WorldEntitiesEnumerator(Entity[] worldEntities)
            {
                entities = worldEntities;
                index = 0;
            }

            public bool MoveNext()
            {
                index++;
                return index < entities.Length - 1 && entities[index] != null;
            }

            public void Reset()
            {
                index = 0;
            }

            public Entity Current => entities[index];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        public CompiledQuery(Filter filter)
        {
            this.filter = filter;
            hasFilter = filter.typeID != -1;
        }

        public bool IsEmpty()
        {
            if (!hasFilter)
                return filter.world.entities.Length == 0;

            return filter.IsEmpty();
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            if (!hasFilter)
                return new WorldEntitiesEnumerator(filter.world.entities);

            return filter.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public NativeFilter AsNative()
        {
            return filter.AsNative();
        }
    }
}