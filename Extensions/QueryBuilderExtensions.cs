using System;

namespace Scellecs.Morpeh
{
    public static class QueryBuilderExtensions
    {
        public static QueryBuilder CreateQuery(this IQuerySystem querySystem)
        {
            return new QueryBuilder(querySystem);
        }
        
#region Alternatives

        public static QueryBuilder Also(this QueryBuilder queryBuilder, Action<FilterBuilder> filterCallback)
        {
            filterCallback?.Invoke(queryBuilder.filterBuilder);
            return queryBuilder;
        }

        public static QueryBuilder With<T>(this QueryBuilder queryBuilder) where T : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.With<T>();
            return queryBuilder;
        }

        public static QueryBuilder Without<T>(this QueryBuilder queryBuilder) where T : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.Without<T>();
            return queryBuilder;
        }

#endregion

#region WithAll

        public static QueryBuilder WithAll<T1>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.With<T1>();
            return queryBuilder;
        }

        public static QueryBuilder WithAll<T1, T2>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.With<T1>().With<T2>();
            return queryBuilder;
        }

        public static QueryBuilder WithAll<T1, T2, T3>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.With<T1>().With<T2>().With<T3>();
            return queryBuilder;
        }

        public static QueryBuilder WithAll<T1, T2, T3, T4>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.With<T1>().With<T2>().With<T3>().With<T4>();
            return queryBuilder;
        }

        public static QueryBuilder WithAll<T1, T2, T3, T4, T5>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>();
            return queryBuilder;
        }

        public static QueryBuilder WithAll<T1, T2, T3, T4, T5, T6>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>().With<T6>();
            return queryBuilder;
        }

        public static QueryBuilder WithAll<T1, T2, T3, T4, T5, T6, T7>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>().With<T6>().With<T7>();
            return queryBuilder;
        }

        public static QueryBuilder WithAll<T1, T2, T3, T4, T5, T6, T7, T8>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>().With<T6>().With<T7>().With<T8>();
            return queryBuilder;
        }

#endregion

#region WithNone

        public static QueryBuilder WithNone<T1>(this QueryBuilder queryBuilder) where T1 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.Without<T1>();
            return queryBuilder;
        }

        public static QueryBuilder WithNone<T1, T2>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.Without<T1>().Without<T2>();
            return queryBuilder;
        }

        public static QueryBuilder WithNone<T1, T2, T3>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.Without<T1>().Without<T2>().Without<T3>();
            return queryBuilder;
        }

        public static QueryBuilder WithNone<T1, T2, T3, T4>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.Without<T1>().Without<T2>().Without<T3>().Without<T4>();
            return queryBuilder;
        }

        public static QueryBuilder WithNone<T1, T2, T3, T4, T5>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>();
            return queryBuilder;
        }

        public static QueryBuilder WithNone<T1, T2, T3, T4, T5, T6>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>().Without<T6>();
            return queryBuilder;
        }

        public static QueryBuilder WithNone<T1, T2, T3, T4, T5, T6, T7>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>().Without<T6>().Without<T7>();
            return queryBuilder;
        }

        public static QueryBuilder WithNone<T1, T2, T3, T4, T5, T6, T7, T8>(this QueryBuilder queryBuilder)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            queryBuilder.filterBuilder = queryBuilder.filterBuilder.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>().Without<T6>().Without<T7>().Without<T8>();
            return queryBuilder;
        }

#endregion

#region ForAll

        /// <summary>
        /// Executes a callback once on every update if filter is no empty
        /// </summary>
        public static QueryBuilder ForAll(this QueryBuilder queryBuilder, Action callback)
        {
            var filter = queryBuilder.Build();
            queryBuilder.System.AddExecutor(() =>
            {
                if (filter.IsEmpty())
                    return;

                callback.Invoke();
            });
            return queryBuilder;
        }

        public static QueryBuilder ForAll(this QueryBuilder queryBuilder, Action<Filter> callback)
        {
            var filter = queryBuilder.Build();
            queryBuilder.System.AddExecutor(() =>
            {
                if (filter.IsEmpty())
                    return;

                callback.Invoke(filter.filter);
            });
            return queryBuilder;
        }

#endregion

#region ForEach

        // ------------------------------------------------- //
        // 0 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEach(this QueryBuilder queryBuilder, Callbacks.E callback)
        {
            var filter = queryBuilder.Build();

            queryBuilder.System.AddExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    callback.Invoke(entity);
                }
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 1 parameter
        // ------------------------------------------------- //

        public static QueryBuilder ForEach<T1>(this QueryBuilder queryBuilder, Callbacks.EC<T1> callback)
            where T1 : struct, IComponent
        {
            var filter = queryBuilder.Build();
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            queryBuilder.System.AddExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    callback.Invoke(entity, ref componentT1);
                }
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEach<T1>(this QueryBuilder queryBuilder, Callbacks.C<T1> callback)
            where T1 : struct, IComponent
        {
            var filter = queryBuilder.Build();
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            queryBuilder.System.AddExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    callback.Invoke(ref componentT1);
                }
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 2 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEach<T1, T2>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            var filter = queryBuilder.Build();
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            queryBuilder.System.AddExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    callback.Invoke(entity, ref componentT1, ref componentT2);
                }
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEach<T1, T2>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            var filter = queryBuilder.Build();
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            queryBuilder.System.AddExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    callback.Invoke(ref componentT1, ref componentT2);
                }
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 3 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEach<T1, T2, T3>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            var filter = queryBuilder.Build();
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            queryBuilder.System.AddExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3);
                }
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEach<T1, T2, T3>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            var filter = queryBuilder.Build();
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            queryBuilder.System.AddExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3);
                }
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 4 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEach<T1, T2, T3, T4>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3, T4> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            var filter = queryBuilder.Build();
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            queryBuilder.System.AddExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    ref var componentT4 = ref stashT4.Get(entity);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3, ref componentT4);
                }
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEach<T1, T2, T3, T4>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3, T4> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            var filter = queryBuilder.Build();
            if (!queryBuilder.skipValidationEnabled)
                QueryHelper.ValidateRequest(queryBuilder, filter, QueryHelper.GetRequestedTypeInfo<T1>(), QueryHelper.GetRequestedTypeInfo<T2>(),
                    QueryHelper.GetRequestedTypeInfo<T3>(), QueryHelper.GetRequestedTypeInfo<T4>());

            var stashT1 = queryBuilder.World.GetStash<T1>();
            var stashT2 = queryBuilder.World.GetStash<T2>();
            var stashT3 = queryBuilder.World.GetStash<T3>();
            var stashT4 = queryBuilder.World.GetStash<T4>();
            queryBuilder.System.AddExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    ref var componentT4 = ref stashT4.Get(entity);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3, ref componentT4);
                }
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 5 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEach<T1, T2, T3, T4, T5>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3, T4, T5> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            var filter = queryBuilder.Build();
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
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    ref var componentT4 = ref stashT4.Get(entity);
                    ref var componentT5 = ref stashT5.Get(entity);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5);
                }
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEach<T1, T2, T3, T4, T5>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3, T4, T5> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            var filter = queryBuilder.Build();
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
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    ref var componentT4 = ref stashT4.Get(entity);
                    ref var componentT5 = ref stashT5.Get(entity);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5);
                }
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 6 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEach<T1, T2, T3, T4, T5, T6>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3, T4, T5, T6> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            var filter = queryBuilder.Build();
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
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    ref var componentT4 = ref stashT4.Get(entity);
                    ref var componentT5 = ref stashT5.Get(entity);
                    ref var componentT6 = ref stashT6.Get(entity);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6);
                }
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEach<T1, T2, T3, T4, T5, T6>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3, T4, T5, T6> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            var filter = queryBuilder.Build();
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
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    ref var componentT4 = ref stashT4.Get(entity);
                    ref var componentT5 = ref stashT5.Get(entity);
                    ref var componentT6 = ref stashT6.Get(entity);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6);
                }
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 7 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEach<T1, T2, T3, T4, T5, T6, T7>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3, T4, T5, T6, T7> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            var filter = queryBuilder.Build();
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
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    ref var componentT4 = ref stashT4.Get(entity);
                    ref var componentT5 = ref stashT5.Get(entity);
                    ref var componentT6 = ref stashT6.Get(entity);
                    ref var componentT7 = ref stashT7.Get(entity);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6, ref componentT7);
                }
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEach<T1, T2, T3, T4, T5, T6, T7>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3, T4, T5, T6, T7> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            var filter = queryBuilder.Build();
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
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    ref var componentT4 = ref stashT4.Get(entity);
                    ref var componentT5 = ref stashT5.Get(entity);
                    ref var componentT6 = ref stashT6.Get(entity);
                    ref var componentT7 = ref stashT7.Get(entity);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6, ref componentT7);
                }
            });
            return queryBuilder;
        }

        // ------------------------------------------------- //
        // 8 parameters
        // ------------------------------------------------- //

        public static QueryBuilder ForEach<T1, T2, T3, T4, T5, T6, T7, T8>(this QueryBuilder queryBuilder, Callbacks.EC<T1, T2, T3, T4, T5, T6, T7, T8> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            var filter = queryBuilder.Build();
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
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    ref var componentT4 = ref stashT4.Get(entity);
                    ref var componentT5 = ref stashT5.Get(entity);
                    ref var componentT6 = ref stashT6.Get(entity);
                    ref var componentT7 = ref stashT7.Get(entity);
                    ref var componentT8 = ref stashT8.Get(entity);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6, ref componentT7, ref componentT8);
                }
            });
            return queryBuilder;
        }

        public static QueryBuilder ForEach<T1, T2, T3, T4, T5, T6, T7, T8>(this QueryBuilder queryBuilder, Callbacks.C<T1, T2, T3, T4, T5, T6, T7, T8> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            var filter = queryBuilder.Build();
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
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    ref var componentT4 = ref stashT4.Get(entity);
                    ref var componentT5 = ref stashT5.Get(entity);
                    ref var componentT6 = ref stashT6.Get(entity);
                    ref var componentT7 = ref stashT7.Get(entity);
                    ref var componentT8 = ref stashT8.Get(entity);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3, ref componentT4, ref componentT5, ref componentT6, ref componentT7, ref componentT8);
                }
            });
            return queryBuilder;
        }

#endregion
    }
}