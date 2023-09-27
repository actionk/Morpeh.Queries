using System;
using Scellecs.Morpeh.Collections;
using Unity.Collections;
using Unity.Jobs;

namespace Scellecs.Morpeh
{
    public abstract class QuerySystem : ISystem, IQuerySystem
    {
        private readonly FastList<Action> m_executors = new();

        public World World { get; set; }
        protected float deltaTime;

#if UNITY_EDITOR
        private bool m_isConfiguringFinished;
#endif

        public virtual void OnAwake()
        {
            Configure();
#if UNITY_EDITOR
            m_isConfiguringFinished = true;
#endif
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

#if MORPEH_BURST
        /// <summary>
        /// As the world can contain multple jobs, we have to wait until all of them are complete.
        /// By default, we will be using World's JobHandle to wait for all the jobs. In this case, we combine the existing JobHandle
        /// from world and JobHandles from this world
        /// </summary>
        private void UpdateJobHandles()
        {
            if (m_waitUntilAllPreviousJobsCompleted)
                World.JobHandle.Complete();
            
            if (m_queryBuilderJobHandles.length == 0) 
                return;
            
            m_jobHandles.Clear();
            var hasWorldJobHandle = !World.JobHandle.IsCompleted;
            
            // collecting unfinished jobs from this system
            for (var i = 0; i < m_queryBuilderJobHandles.length; i++)
            {
                var jobHandle = m_queryBuilderJobHandles.data[i];
                if (jobHandle.jobHandle.IsCompleted)
                    continue;

                m_jobHandles.Add(jobHandle.jobHandle);
            }

            var jobHandles = new NativeArray<JobHandle>(m_jobHandles.length + (hasWorldJobHandle ? 1 : 0), Allocator.Temp);
            for (var i = 0; i < m_jobHandles.length; i++)
                jobHandles[i] = m_jobHandles.data[i];

            if (hasWorldJobHandle)
                jobHandles[m_jobHandles.length] = World.JobHandle;
            
            var allDependenciesJobHandle = JobHandle.CombineDependencies(jobHandles);
            
            // if there is a job that has [WaitUntilInnerJobsCompleted] activated, we should force this system to wait until jobs are completed
            // otherwise, let the world wait for it in the end of the update (or until some other system wait for it)
            if (m_waitUntilAllInnerJobsCompleted)
                allDependenciesJobHandle.Complete();
            else
                World.JobHandle = allDependenciesJobHandle;
            
            jobHandles.Dispose();
        }

        protected QueryBuilder CreateQuery()
        {
            return new QueryBuilder(this);
        }

        public virtual bool IsUpdatedEveryFrame => true;

        void IQuerySystem.AddExecutor(Action newQueryExecutor)
        {
#if UNITY_EDITOR
            if (m_isConfiguringFinished)
                throw new Exception($"You shouldn't add executors after [{nameof(Configure)}] finished, this leads to bugs!");
#endif
            m_executors.Add(newQueryExecutor);
        }

        private readonly FastList<QueryBuilderJobHandle> m_queryBuilderJobHandles = new();
        private readonly FastList<JobHandle> m_jobHandles = new();
        private bool m_waitUntilAllInnerJobsCompleted;
        private bool m_waitUntilAllPreviousJobsCompleted;

        void IQuerySystem.AddJobHandle(QueryBuilderJobHandle jobHandles)
        {
#if UNITY_EDITOR
            if (m_isConfiguringFinished)
                throw new Exception($"You shouldn't add executors after [{nameof(Configure)}] finished, this leads to bugs!");
#endif
            m_queryBuilderJobHandles.Add(jobHandles);
        }

        protected void WaitUntilInnerJobsCompleted(bool enabled = true)
        {
            m_waitUntilAllInnerJobsCompleted = enabled;
        }

        protected void WaitUntilPreviousJobsCompleted(bool enabled = true)
        {
            m_waitUntilAllPreviousJobsCompleted = enabled;
        }
#endif
    }
}