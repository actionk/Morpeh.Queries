using System;
using Scellecs.Morpeh.Collections;

namespace Scellecs.Morpeh
{
    public abstract class QuerySystem : ISystem
    {
        private readonly FastList<Action> m_executors = new();
        public World World { get; set; }
        protected float deltaTime;

        public virtual void OnAwake()
        {
            Configure();
        }
        
        protected abstract void Configure();

        public virtual void Dispose()
        {
        }

        public virtual void OnUpdate(float newDeltaTime)
        {
            deltaTime = newDeltaTime;
            foreach (var executor in m_executors)
                executor.Invoke();
        }

        protected QueryConfigurer CreateQuery()
        {
            return new QueryConfigurer(this);
        }

        internal void AddExecutor(Action newQueryExecutor)
        {
            m_executors.Add(newQueryExecutor);
        }
    }
}