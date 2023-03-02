#if MORPEH_BURST
using System;
using System.Reflection;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Native;
using Unity.Jobs;

namespace Scellecs.Morpeh
{
    public static class QueryBuilderJobsExtensions
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

        public static QueryBuilderJobHandle ScheduleJob<T>(this QueryBuilder queryBuilder, QueryBuilderJobHandle waitForJobHandle)
            where T : struct, IJobParallelFor
        {
            return ScheduleJob<T>(queryBuilder, 64, waitForJobHandle);
        }

        public static QueryBuilderJobHandle ScheduleJob<T>(this QueryBuilder queryBuilder, int batchCount = 64, QueryBuilderJobHandle waitForJobHandle = default)
            where T : struct, IJobParallelFor
        {
            FieldInfo nativeFilterField = null;
            var stashFields = new FastList<FieldInfo>();
            var stashes = new FastList<IStashWrapper>();

            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ScheduleJob)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

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

                    QueryHelper.ValidateRequest(queryBuilder, filter, new QueryHelper.RequestedTypeInfo(requestedComponentType, typeInfo.id));

                    stashFields.Add(field);
                    var stash = queryBuilder.World.GetStash(typeInfo.id);
                    var stashWrapper = (IStashWrapper)Activator.CreateInstance(typeof(StashWrapper<>).MakeGenericType(requestedComponentType), stash);
                    stashes.Add(stashWrapper);
                }
            }

            var jobHandle = new QueryBuilderJobHandle();
            queryBuilder.System.AddExecutor(() =>
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

            queryBuilder.System.AddJobHandle(jobHandle);
            return jobHandle;
        }

        // ------------------------------------------------- //
        // 1 parameter
        // ------------------------------------------------- //

        public delegate void E<T1>(NativeFilter entities, NativeStash<T1> component1)
            where T1 : unmanaged, IComponent;

        public static QueryBuilder ForEachNative<T1>(this QueryBuilder queryBuilder, E<T1> callback)
            where T1 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachNative)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");
            
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative());
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 2 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2>(NativeFilter entities, NativeStash<T1> component1, NativeStash<T2> component2)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent;

        public static QueryBuilder ForEachNative<T1, T2>(this QueryBuilder queryBuilder, E<T1, T2> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachNative)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative());
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 3 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3>(NativeFilter entities, NativeStash<T1> component1, NativeStash<T2> component2, NativeStash<T3> component3)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent;

        public static QueryBuilder ForEachNative<T1, T2, T3>(this QueryBuilder queryBuilder, E<T1, T2, T3> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachNative)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative());
            });
            return queryBuilder;
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

        public static QueryBuilder ForEachNative<T1, T2, T3, T4>(this QueryBuilder queryBuilder, E<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachNative)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative(), stashT4.AsNative());
            });
            return queryBuilder;
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

        public static QueryBuilder ForEachNative<T1, T2, T3, T4, T5>(this QueryBuilder queryBuilder, E<T1, T2, T3, T4, T5> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachNative)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>(), QueryHelper.GetRequestedTypeInfo<T5>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            var stashT5 = queryBuilder.World.GetStash<T5>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative(), stashT4.AsNative(), stashT5.AsNative());
            });
            return queryBuilder;
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

        public static QueryBuilder ForEachNative<T1, T2, T3, T4, T5, T6>(this QueryBuilder queryBuilder, E<T1, T2, T3, T4, T5, T6> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
            where T6 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachNative)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>(), QueryHelper.GetRequestedTypeInfo<T5>(),
                    QueryHelper.GetRequestedTypeInfo<T6>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            var stashT5 = queryBuilder.World.GetStash<T5>();
            var stashT6 = queryBuilder.World.GetStash<T6>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                callback.Invoke(nativeFilter, stashT1.AsNative(), stashT2.AsNative(), stashT3.AsNative(), stashT4.AsNative(),
                    stashT5.AsNative(), stashT6.AsNative());
            });
            return queryBuilder;
        }
    }
}
#endif