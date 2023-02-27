#if MORPEH_BURST
using System;
using System.Reflection;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Native;
using Unity.Jobs;

namespace Scellecs.Morpeh
{
    public static class QueryConfigurerJobsExtensions
    {
        // ------------------------------------------------- //
        // Dynamic parameters
        // ------------------------------------------------- //

        private interface IStashWrapper
        {
            public object GetNativeStash();
            public Stash GetStash();
        }

        private struct StashWrapper<T> : IStashWrapper where T : unmanaged, IComponent
        {
            public Stash<T> stash;

            public StashWrapper(Stash<T> stash)
            {
                this.stash = stash;
            }

            public object GetNativeStash()
            {
                return stash.AsNative();
            }

            public Stash GetStash()
            {
                return stash;
            }
        }

        public static QueryConfigurerJobHandle ScheduleJob<T>(this QueryConfigurer queryConfigurer, QueryConfigurerJobHandle waitForJobHandle)
            where T : struct, IJobParallelFor
        {
            return ScheduleJob<T>(queryConfigurer, 64, waitForJobHandle);
        }

        public static QueryConfigurerJobHandle ScheduleJob<T>(this QueryConfigurer queryConfigurer, int batchCount = 64, QueryConfigurerJobHandle waitForJobHandle = default)
            where T : struct, IJobParallelFor
        {
            FieldInfo nativeFilterField = null;
            var stashFields = new FastList<FieldInfo>();
            var stashes = new FastList<IStashWrapper>();

            var filter = queryConfigurer.Filter;
            var type = typeof(T);
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType == typeof(NativeFilter))
                    nativeFilterField = field;
                else if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(NativeStash<>))
                {
                    var requestedComponentType = field.FieldType.GetGenericArguments()[0];
                    var infoFieldInfo = typeof(TypeIdentifier<>)
                        .MakeGenericType(requestedComponentType)
                        .GetField("info", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
                    var typeInfo = (CommonTypeIdentifier.TypeInfo)infoFieldInfo!.GetValue(null);

                    QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, new QueryConfigurerHelper.RequestedTypeInfo(requestedComponentType, typeInfo.id));

                    stashFields.Add(field);
                    var stash = queryConfigurer.World.GetStash(typeInfo.id);
                    var stashWrapper = (IStashWrapper)Activator.CreateInstance(typeof(StashWrapper<>).MakeGenericType(requestedComponentType), stash);
                    stashes.Add(stashWrapper);
                }
            }

            var jobHandle = new QueryConfigurerJobHandle();
            queryConfigurer.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var parallelJob = new T();
                object parallelJobReference = parallelJob;
                nativeFilterField?.SetValue(parallelJobReference, nativeFilter);
                for (var i = 0; i < stashFields.length; i++)
                    stashFields.data[i].SetValue(parallelJobReference, stashes.data[i].GetNativeStash());
                parallelJob = (T)parallelJobReference;

                var parallelJobHandle = parallelJob.Schedule(nativeFilter.length, batchCount, waitForJobHandle?.jobHandle ?? default);
                jobHandle.jobHandle = parallelJobHandle;
            });

            queryConfigurer.System.AddJobHandle(jobHandle);
            return jobHandle;
        }

        // ------------------------------------------------- //
        // 1 parameter
        // ------------------------------------------------- //

        public delegate void E<T1>(NativeFilter entities, NativeStash<T1> component1)
            where T1 : unmanaged, IComponent;

        public static QueryConfigurer ForEachNative<T1>(this QueryConfigurer queryConfigurer, E<T1> callback)
            where T1 : unmanaged, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                QueryConfigurerHelper.ValidateRequest(queryConfigurer, filter, QueryConfigurerHelper.GetRequestedTypeInfo<T1>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            queryConfigurer.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative());
            });
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 2 parameters
        // ------------------------------------------------- //

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
            queryConfigurer.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative());
            });
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 3 parameters
        // ------------------------------------------------- //

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
            queryConfigurer.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative());
            });
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 4 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3, T4>(NativeFilter entities, NativeStash<T1> component1, NativeStash<T2> component2, NativeStash<T3> component3,
            NativeStash<T4> component4)
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
            queryConfigurer.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative(), stashT4.AsNative());
            });
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 5 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3, T4, T5>(NativeFilter entities, NativeStash<T1> component1, NativeStash<T2> component2, NativeStash<T3> component3,
            NativeStash<T4> component4,
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
            queryConfigurer.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative(), stashT4.AsNative(), stashT5.AsNative());
            });
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 6 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3, T4, T5, T6>(NativeFilter entities, NativeStash<T1> component1, NativeStash<T2> component2, NativeStash<T3> component3,
            NativeStash<T4> component4,
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
            queryConfigurer.System.AddExecutor(() =>
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