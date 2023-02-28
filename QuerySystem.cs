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
            UpdateJobHandles();
#endif
        }

        /// <summary>
        /// As the world can contain multple jobs, we have to wait until all of them are complete.
        /// By default, we will be using World's JobHandle to wait for all the jobs. In this case, we combine the existing JobHandle
        /// from world and JobHandles from this world
        /// </summary>
        private void UpdateJobHandles()
        {
            if (m_jobHandles.length == 0) 
                return;
            
            var hasWorldJobHandle = !World.JobHandle.IsCompleted;
            var jobHandlesLength = m_jobHandles.length + (hasWorldJobHandle ? 1 : 0);
            var jobHandles = new NativeArray<JobHandle>(jobHandlesLength, Allocator.Temp);
            for (var i = 0; i < m_jobHandles.length; i++)
                jobHandles[i] = m_jobHandles.data[i].jobHandle;
            if (hasWorldJobHandle)
                jobHandles[m_jobHandles.length] = World.JobHandle;
            World.JobHandle = JobHandle.CombineDependencies(jobHandles);
            jobHandles.Dispose();
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