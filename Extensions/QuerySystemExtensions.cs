using Unity.Jobs;

namespace Scellecs.Morpeh
{
    public static class QuerySystemExtensions
    {
        public delegate void PreparationDelegate<T>(ref T jobToPrepare) where T : struct, IJob;

        public static QueryBuilderJobHandle ScheduleJob<T>(this IQuerySystem querySystem,
            PreparationDelegate<T> preparationDelegate = default,
            QueryBuilderJobHandle waitForJobHandle = default
        ) where T : struct, IJob
        {
            var queryJobHandle = new QueryBuilderJobHandle();
            querySystem.AddJobHandle(queryJobHandle);
            querySystem.AddExecutor(() =>
            {
                var job = new T();
                preparationDelegate?.Invoke(ref job);
                queryJobHandle.jobHandle = job.Schedule(waitForJobHandle?.jobHandle ?? default);
            });
            return queryJobHandle;
        }
    }
}