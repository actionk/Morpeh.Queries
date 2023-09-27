using System;
using System.Threading.Tasks;
using Scellecs.Morpeh.Native;

namespace Scellecs.Morpeh
{
    public static class QueryBuilderParallelExtensions
    {
        // ------------------------------------------------- //
        // 1 parameter
        // ------------------------------------------------- //
        
        public static QueryBuilder ForEachParallel<T1>(this QueryBuilder queryBuilder, Callbacks.C<T1> callback)
            where T1 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");
            
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    callback.Invoke(ref componentT1);
                });
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEachParallel<T1>(this QueryBuilder queryBuilder, Callbacks.EC<T1> callback)
            where T1 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");
            
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    var entity = filter.World.GetEntity(entityId.id);
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    callback.Invoke(entity, ref componentT1);
                });
            });
            return queryBuilder;
        }
        
        // ------------------------------------------------- //
        // 2 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEachParallel<T1, T2>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    var entity = filter.World.GetEntity(entityId.id);
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    callback.Invoke(entity, ref componentT1, ref componentT2);
                });
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEachParallel<T1, T2>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    callback.Invoke(ref componentT1, ref componentT2);
                });
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 3 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEachParallel<T1, T2, T3>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    var entity = filter.World.GetEntity(entityId.id);
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3);
                });
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEachParallel<T1, T2, T3>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3);
                });
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 4 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEachParallel<T1, T2, T3, T4>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

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
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                var nativeStashT4 = stashT4.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    var entity = filter.World.GetEntity(entityId.id);
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    ref var componentT4 = ref nativeStashT4.Get(entityId);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3, ref componentT4);
                });
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEachParallel<T1, T2, T3, T4>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

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
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                var nativeStashT4 = stashT4.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    ref var componentT4 = ref nativeStashT4.Get(entityId);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3, ref componentT4);
                });
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 5 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEachParallel<T1, T2, T3, T4, T5>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3, T4, T5> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter,
                    QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(), QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>(),
                    QueryHelper.GetRequestedTypeInfo<T5>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            var stashT5 = queryBuilder.World.GetStash<T5>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                var nativeStashT4 = stashT4.AsNative();
                var nativeStashT5 = stashT5.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    var entity = filter.World.GetEntity(entityId.id);
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    ref var componentT4 = ref nativeStashT4.Get(entityId);
                    ref var componentT5 = ref nativeStashT5.Get(entityId);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5);
                });
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEachParallel<T1, T2, T3, T4, T5>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3, T4, T5> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>(),
                    QueryHelper.GetRequestedTypeInfo<T5>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            var stashT5 = queryBuilder.World.GetStash<T5>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                var nativeStashT4 = stashT4.AsNative();
                var nativeStashT5 = stashT5.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    ref var componentT4 = ref nativeStashT4.Get(entityId);
                    ref var componentT5 = ref nativeStashT5.Get(entityId);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5);
                });
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 6 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEachParallel<T1, T2, T3, T4, T5, T6>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3, T4, T5, T6> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
            where T6 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter,
                    QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(), QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>(),
                    QueryHelper.GetRequestedTypeInfo<T5>(), QueryHelper.GetRequestedTypeInfo<T6>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            var stashT5 = queryBuilder.World.GetStash<T5>();
            var stashT6 = queryBuilder.World.GetStash<T6>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                var nativeStashT4 = stashT4.AsNative();
                var nativeStashT5 = stashT5.AsNative();
                var nativeStashT6 = stashT6.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    var entity = filter.World.GetEntity(entityId.id);
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    ref var componentT4 = ref nativeStashT4.Get(entityId);
                    ref var componentT5 = ref nativeStashT5.Get(entityId);
                    ref var componentT6 = ref nativeStashT6.Get(entityId);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6);
                });
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEachParallel<T1, T2, T3, T4, T5, T6>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3, T4, T5, T6> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
            where T6 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>(),
                    QueryHelper.GetRequestedTypeInfo<T5>(), QueryHelper.GetRequestedTypeInfo<T6>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            var stashT5 = queryBuilder.World.GetStash<T5>();
            var stashT6 = queryBuilder.World.GetStash<T6>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                var nativeStashT4 = stashT4.AsNative();
                var nativeStashT5 = stashT5.AsNative();
                var nativeStashT6 = stashT6.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    var entity = filter.World.GetEntity(entityId.id);
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    ref var componentT4 = ref nativeStashT4.Get(entityId);
                    ref var componentT5 = ref nativeStashT5.Get(entityId);
                    ref var componentT6 = ref nativeStashT6.Get(entityId);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6);
                });
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 7 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEachParallel<T1, T2, T3, T4, T5, T6, T7>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3, T4, T5, T6, T7> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
            where T6 : unmanaged, IComponent
            where T7 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter,
                    QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(), QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>(),
                    QueryHelper.GetRequestedTypeInfo<T5>(), QueryHelper.GetRequestedTypeInfo<T6>(), QueryHelper.GetRequestedTypeInfo<T7>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            var stashT5 = queryBuilder.World.GetStash<T5>();
            var stashT6 = queryBuilder.World.GetStash<T6>();
            var stashT7 = queryBuilder.World.GetStash<T7>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                var nativeStashT4 = stashT4.AsNative();
                var nativeStashT5 = stashT5.AsNative();
                var nativeStashT6 = stashT6.AsNative();
                var nativeStashT7 = stashT7.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    var entity = filter.World.GetEntity(entityId.id);
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    ref var componentT4 = ref nativeStashT4.Get(entityId);
                    ref var componentT5 = ref nativeStashT5.Get(entityId);
                    ref var componentT6 = ref nativeStashT6.Get(entityId);
                    ref var componentT7 = ref nativeStashT7.Get(entityId);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6, ref componentT7);
                });
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEachParallel<T1, T2, T3, T4, T5, T6, T7>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3, T4, T5, T6, T7> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
            where T6 : unmanaged, IComponent
            where T7 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>(),
                    QueryHelper.GetRequestedTypeInfo<T5>(), QueryHelper.GetRequestedTypeInfo<T6>(), QueryHelper.GetRequestedTypeInfo<T7>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            var stashT5 = queryBuilder.World.GetStash<T5>();
            var stashT6 = queryBuilder.World.GetStash<T6>();
            var stashT7 = queryBuilder.World.GetStash<T7>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                var nativeStashT4 = stashT4.AsNative();
                var nativeStashT5 = stashT5.AsNative();
                var nativeStashT6 = stashT6.AsNative();
                var nativeStashT7 = stashT7.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    ref var componentT4 = ref nativeStashT4.Get(entityId);
                    ref var componentT5 = ref nativeStashT5.Get(entityId);
                    ref var componentT6 = ref nativeStashT6.Get(entityId);
                    ref var componentT7 = ref nativeStashT7.Get(entityId);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6, ref componentT7);
                });
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 8 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEachParallel<T1, T2, T3, T4, T5, T6, T7, T8>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3, T4, T5, T6, T7, T8> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
            where T6 : unmanaged, IComponent
            where T7 : unmanaged, IComponent
            where T8 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter,
                    QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(), QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>(),
                    QueryHelper.GetRequestedTypeInfo<T5>(), QueryHelper.GetRequestedTypeInfo<T6>(), QueryHelper.GetRequestedTypeInfo<T7>(), QueryHelper.GetRequestedTypeInfo<T8>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            var stashT5 = queryBuilder.World.GetStash<T5>();
            var stashT6 = queryBuilder.World.GetStash<T6>();
            var stashT7 = queryBuilder.World.GetStash<T7>();
            var stashT8 = queryBuilder.World.GetStash<T8>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                var nativeStashT4 = stashT4.AsNative();
                var nativeStashT5 = stashT5.AsNative();
                var nativeStashT6 = stashT6.AsNative();
                var nativeStashT7 = stashT7.AsNative();
                var nativeStashT8 = stashT8.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    var entity = filter.World.GetEntity(entityId.id);
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    ref var componentT4 = ref nativeStashT4.Get(entityId);
                    ref var componentT5 = ref nativeStashT5.Get(entityId);
                    ref var componentT6 = ref nativeStashT6.Get(entityId);
                    ref var componentT7 = ref nativeStashT7.Get(entityId);
                    ref var componentT8 = ref nativeStashT8.Get(entityId);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6, ref componentT7, ref componentT8);
                });
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEachParallel<T1, T2, T3, T4, T5, T6, T7, T8>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3, T4, T5, T6, T7, T8> callback)
            where T1 : unmanaged, IComponent
            where T2 : unmanaged, IComponent
            where T3 : unmanaged, IComponent
            where T4 : unmanaged, IComponent
            where T5 : unmanaged, IComponent
            where T6 : unmanaged, IComponent
            where T7 : unmanaged, IComponent
            where T8 : unmanaged, IComponent
        {
            var filter = queryBuilder.Build();
            if (!filter.hasFilter)
                throw new NotImplementedException($"You're not allowed to use [{nameof(ForEachParallel)}] on an empty filter in [{queryBuilder.System.GetType().Name}]");

            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter,
                    QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(), QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>(),
                    QueryHelper.GetRequestedTypeInfo<T5>(), QueryHelper.GetRequestedTypeInfo<T6>(), QueryHelper.GetRequestedTypeInfo<T7>(), QueryHelper.GetRequestedTypeInfo<T8>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            var stashT5 = queryBuilder.World.GetStash<T5>();
            var stashT6 = queryBuilder.World.GetStash<T6>();
            var stashT7 = queryBuilder.World.GetStash<T7>();
            var stashT8 = queryBuilder.World.GetStash<T8>();
            queryBuilder.System.AddExecutor(() =>
            {
                var nativeFilter = filter.AsNative();
                var nativeStashT1 = stashT1.AsNative();
                var nativeStashT2 = stashT2.AsNative();
                var nativeStashT3 = stashT3.AsNative();
                var nativeStashT4 = stashT4.AsNative();
                var nativeStashT5 = stashT5.AsNative();
                var nativeStashT6 = stashT6.AsNative();
                var nativeStashT7 = stashT7.AsNative();
                var nativeStashT8 = stashT8.AsNative();
                Parallel.For(0, nativeFilter.length, index =>
                {
                    var entityId = nativeFilter[index];
                    ref var componentT1 = ref nativeStashT1.Get(entityId);
                    ref var componentT2 = ref nativeStashT2.Get(entityId);
                    ref var componentT3 = ref nativeStashT3.Get(entityId);
                    ref var componentT4 = ref nativeStashT4.Get(entityId);
                    ref var componentT5 = ref nativeStashT5.Get(entityId);
                    ref var componentT6 = ref nativeStashT6.Get(entityId);
                    ref var componentT7 = ref nativeStashT7.Get(entityId);
                    ref var componentT8 = ref nativeStashT8.Get(entityId);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6, ref componentT7, ref componentT8);
                });
            });
            return queryBuilder;
        }

    }
}