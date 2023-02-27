using System;
using Scellecs.Morpeh.Collections;
using Unity.Collections;
using Unity.Jobs;

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

#if MORPEH_BURST
            if (m_jobHandles.length > 0)
            {
                var jobHandles = new NativeArray<JobHandle>(m_jobHandles.length, Allocator.Temp);
                for (var i = 0; i < m_jobHandles.length; i++)
                    jobHandles[i] = m_jobHandles.data[i].jobHandle;
                JobHandle.CombineDependencies(jobHandles).Complete();
                jobHandles.Dispose();
            }
#endif
        }

        protected QueryBuilder CreateQuery()
        {
            return new QueryBuilder(this);
        }

        internal void AddExecutor(Action newQueryExecutor)
        {
            m_executors.Add(newQueryExecutor);
        }

#if MORPEH_BURST
        private readonly FastList<QueryBuilderJobHandle> m_jobHandles = new();

        internal void AddJobHandle(QueryBuilderJobHandle jobHandles)
        {
            m_jobHandles.Add(jobHandles);
        }
#endif
    }
}