#if MORPEH_BURST
using Scellecs.Morpeh.Native;
using Unity.Jobs;

namespace Scellecs.Morpeh
{
    public interface IEntityQueryJobParallelFor<T1> : IJobParallelFor where T1 : unmanaged, IComponent
    {
        public NativeFilter Entities { get; set; }
        public NativeStash<T1> ComponentT1 { get; set; }
    }
    
    public interface IEntityQueryJobParallelFor<T1, T2> : IJobParallelFor 
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
    {
        public NativeFilter Filter { get; set; }
        public NativeStash<T1> ComponentT1 { get; set; }
        public NativeStash<T2> ComponentT2 { get; set; }
    }
    
    public interface IEntityQueryJobParallelFor<T1, T2, T3> : IJobParallelFor 
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
    {
        public NativeFilter Filter { get; set; }
        public NativeStash<T1> ComponentT1 { get; set; }
        public NativeStash<T2> ComponentT2 { get; set; }
        public NativeStash<T3> ComponentT3 { get; set; }
    }
    
    public interface IEntityQueryJobParallelFor<T1, T2, T3, T4> : IJobParallelFor 
        where T1 : unmanaged, IComponent
        where T2 : unmanaged, IComponent
        where T3 : unmanaged, IComponent
        where T4 : unmanaged, IComponent
    {
        public NativeFilter Filter { get; set; }
        public NativeStash<T1> ComponentT1 { get; set; }
        public NativeStash<T2> ComponentT2 { get; set; }
        public NativeStash<T3> ComponentT3 { get; set; }
        public NativeStash<T4> ComponentT4 { get; set; }
    }

    public static class QueryConfigurerJobsExtensions
    {
        // ------------------------------------------------- //
        // 1 parameter
        // ------------------------------------------------- //
        public static QueryConfigurer ScheduleJob<T, T1>(this QueryConfigurer queryConfigurer, int batchCount = 64)
            where T : struct, IEntityQueryJobParallelFor<T1>
            where T1 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, QueryConfigurerHelper.GetRequestedTypeInfo<T1>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            queryConfigurer.RegisterExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var parallelJob = new T
                {
                    Entities = nativeFilter,
                    ComponentT1 = stashT1.AsNative()
                };
                var parallelJobHandle = parallelJob.Schedule(nativeFilter.length, 64);
                parallelJobHandle.Complete();
            });
            return queryConfigurer;
        }
        
        public delegate void E<T1>(NativeFilter entities, NativeStash<T1> component1) 
            where T1 : unmanaged, IComponent;
        
        public static QueryConfigurer ForEachNative<T1>(this QueryConfigurer queryConfigurer, E<T1> callback)
            where T1 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, QueryConfigurerHelper.GetRequestedTypeInfo<T1>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            queryConfigurer.RegisterExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative());
            });
            return queryConfigurer;
        }
        
        // ------------------------------------------------- //
        // 2 parameters
        // ------------------------------------------------- //
        
        public static QueryConfigurer ScheduleJob<T, T1, T2>(this QueryConfigurer queryConfigurer, int batchCount = 64)
            where T : struct, IEntityQueryJobParallelFor<T1, T2>
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, 
                    QueryConfigurerHelper.GetRequestedTypeInfo<T1>(), QueryConfigurerHelper.GetRequestedTypeInfo<T2>()
                );

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            queryConfigurer.RegisterExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var parallelJob = new T
                {
                    Filter = nativeFilter,
                    ComponentT1 = stashT1.AsNative(),
                    ComponentT2 = stashT2.AsNative()
                };
                var parallelJobHandle = parallelJob.Schedule(nativeFilter.length, batchCount);
                parallelJobHandle.Complete();
            });
            return queryConfigurer;
        }
        
        public delegate void E<T1, T2>(NativeFilter entities, NativeStash<T1> component1, NativeStash<T2> component2) 
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent;
        
        public static QueryConfigurer ForEachNative<T1, T2>(this QueryConfigurer queryConfigurer, E<T1, T2> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, QueryConfigurerHelper.GetRequestedTypeInfo<T1>(), QueryConfigurerHelper.GetRequestedTypeInfo<T2>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            queryConfigurer.RegisterExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative());
            });
            return queryConfigurer;
        }
        
        // ------------------------------------------------- //
        // 3 parameters
        // ------------------------------------------------- //
        public static QueryConfigurer ScheduleJob<T, T1, T2, T3>(this QueryConfigurer queryConfigurer, int batchCount = 64)
            where T : struct, IEntityQueryJobParallelFor<T1, T2, T3>
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, 
                    QueryConfigurerHelper.GetRequestedTypeInfo<T1>(), QueryConfigurerHelper.GetRequestedTypeInfo<T2>(), QueryConfigurerHelper.GetRequestedTypeInfo<T3>()
                );

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            queryConfigurer.RegisterExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var parallelJob = new T
                {
                    Filter = nativeFilter,
                    ComponentT1 = stashT1.AsNative(),
                    ComponentT2 = stashT2.AsNative(),
                    ComponentT3 = stashT3.AsNative(),
                };
                var parallelJobHandle = parallelJob.Schedule(nativeFilter.length, batchCount);
                parallelJobHandle.Complete();
            });
            return queryConfigurer;
        }

        public delegate void E<T1, T2, T3>(NativeFilter entities, NativeStash<T1> component1, NativeStash<T2> component2, NativeStash<T3> component3)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent;
        
        public static QueryConfigurer ForEachNative<T1, T2, T3>(this QueryConfigurer queryConfigurer, E<T1, T2, T3> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, QueryConfigurerHelper.GetRequestedTypeInfo<T1>(), QueryConfigurerHelper.GetRequestedTypeInfo<T2>(), 
                    QueryConfigurerHelper.GetRequestedTypeInfo<T3>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            queryConfigurer.RegisterExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative());
            });
            return queryConfigurer;
        }
        
        // ------------------------------------------------- //
        // 4 parameters
        // ------------------------------------------------- //
        public static QueryConfigurer ScheduleJob<T, T1, T2, T3, T4>(this QueryConfigurer queryConfigurer, int batchCount = 64)
            where T : struct, IEntityQueryJobParallelFor<T1, T2, T3, T4>
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, 
                    QueryConfigurerHelper.GetRequestedTypeInfo<T1>(), QueryConfigurerHelper.GetRequestedTypeInfo<T2>(), 
                    QueryConfigurerHelper.GetRequestedTypeInfo<T3>(), QueryConfigurerHelper.GetRequestedTypeInfo<T4>()
                );

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            queryConfigurer.RegisterExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var parallelJob = new T
                {
                    Filter = nativeFilter,
                    ComponentT1 = stashT1.AsNative(),
                    ComponentT2 = stashT2.AsNative(),
                    ComponentT3 = stashT3.AsNative(),
                    ComponentT4 = stashT4.AsNative(),
                };
                var parallelJobHandle = parallelJob.Schedule(nativeFilter.length, batchCount);
                parallelJobHandle.Complete();
            });
            return queryConfigurer;
        }

        public delegate void E<T1, T2, T3, T4>(NativeFilter entities, NativeStash<T1> component1, NativeStash<T2> component2, NativeStash<T3> component3, NativeStash<T4> component4)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent;
        
        public static QueryConfigurer ForEachNative<T1, T2, T3, T4>(this QueryConfigurer queryConfigurer, E<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, QueryConfigurerHelper.GetRequestedTypeInfo<T1>(), QueryConfigurerHelper.GetRequestedTypeInfo<T2>(), 
                    QueryConfigurerHelper.GetRequestedTypeInfo<T3>(), QueryConfigurerHelper.GetRequestedTypeInfo<T4>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            queryConfigurer.RegisterExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative(), stashT4.AsNative());
            });
            return queryConfigurer;
        }
        
        // ------------------------------------------------- //
        // 5 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3, T4, T5>(NativeFilter entities, NativeStash<T1> component1, NativeStash<T2> component2, NativeStash<T3> component3, NativeStash<T4> component4, 
            NativeStash<T5> component5)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent;
        
        public static QueryConfigurer ForEachNative<T1, T2, T3, T4, T5>(this QueryConfigurer queryConfigurer, E<T1, T2, T3, T4, T5> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, QueryConfigurerHelper.GetRequestedTypeInfo<T1>(), QueryConfigurerHelper.GetRequestedTypeInfo<T2>(), 
                    QueryConfigurerHelper.GetRequestedTypeInfo<T3>(), QueryConfigurerHelper.GetRequestedTypeInfo<T4>(), QueryConfigurerHelper.GetRequestedTypeInfo<T5>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            var stashT5 = queryConfigurer.World.GetStash<T5>();
            queryConfigurer.RegisterExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative(), stashT4.AsNative(), stashT5.AsNative());
            });
            return queryConfigurer;
        }
        
        // ------------------------------------------------- //
        // 6 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3, T4, T5, T6>(NativeFilter entities, NativeStash<T1> component1, NativeStash<T2> component2, NativeStash<T3> component3, NativeStash<T4> component4, 
            NativeStash<T5> component5, NativeStash<T6> component6)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
            where T6 : unmanaged, IComponent;
        
        public static QueryConfigurer ForEachNative<T1, T2, T3, T4, T5, T6>(this QueryConfigurer queryConfigurer, E<T1, T2, T3, T4, T5, T6> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
            where T6 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, QueryConfigurerHelper.GetRequestedTypeInfo<T1>(), QueryConfigurerHelper.GetRequestedTypeInfo<T2>(), 
                    QueryConfigurerHelper.GetRequestedTypeInfo<T3>(), QueryConfigurerHelper.GetRequestedTypeInfo<T4>(), QueryConfigurerHelper.GetRequestedTypeInfo<T5>(), 
                    QueryConfigurerHelper.GetRequestedTypeInfo<T6>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            var stashT5 = queryConfigurer.World.GetStash<T5>();
            var stashT6 = queryConfigurer.World.GetStash<T6>();
            queryConfigurer.RegisterExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative(), stashT4.AsNative(), 
                    stashT5.AsNative(), stashT6.AsNative());
            });
            return queryConfigurer;
        }
    }
}
#endif