using System;
using System.Linq;
using UnityEngine;

namespace Scellecs.Morpeh
{
    public static class QueryConfigurerExtensions
    {
        // ------------------------------------------------- //
        // UTILS
        // ------------------------------------------------- //

        private struct RequestedTypeInfo
        {
            public Type type;
            public int typeId;

            public RequestedTypeInfo(Type type) : this()
            {
                this.type = type;
            }

            public RequestedTypeInfo(Type type, int typeId)
            {
                this.type = type;
                this.typeId = typeId;
            }
        }

        private static RequestedTypeInfo GetRequestedTypeInfo<T>() where T : struct, IComponent
        {
            return new RequestedTypeInfo(typeof(T), TypeIdentifier<T>.info.id);
        }

        private static void ValidateRequest(QueryConfigurer queryConfigurer, Filter filter, params RequestedTypeInfo[] requestedTypeInfosToValidate)
        {
            var hasProblems = false;
            foreach (var requestedTypeInfo in requestedTypeInfosToValidate)
            {
                if (!filter.includedTypeIds.Contains(requestedTypeInfo.typeId))
                    Debug.LogError(
                        $"You're expecting a component [<b>{requestedTypeInfo.type.Name}</b>] in your query in [<b>{queryConfigurer.System.GetType().Name}</b>], but the query is <b>missing</b> this parameter. Please add it to the query first!");

                if (filter.excludedTypeIds.Contains(requestedTypeInfo.typeId))
                    Debug.LogError(
                        $"You're expecting a component [<b>{requestedTypeInfo.type.Name}</b>] in your query in [<b>{queryConfigurer.System.GetType().Name}</b>], but the query is <b>deliberately excluded</b> this parameter. Please remove it from the query or from the ForEach lambda!");
            }

            if (hasProblems)
                throw new Exception($"There were problems when configuring a query for [<b>{queryConfigurer.System.GetType().Name}</b>]. Please fix those first");
        }

        // ------------------------------------------------- //
        // 1 parameter
        // ------------------------------------------------- //

        public delegate void EP1<T1>(Entity entity, ref T1 component);

        public static QueryConfigurer ForEach<T1>(this QueryConfigurer queryConfigurer, EP1<T1> callback)
            where T1 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            queryConfigurer.RegisterExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    callback.Invoke(entity, ref componentT1);
                }
            });
            return queryConfigurer;
        }

        public delegate void P1<T1>(ref T1 component);

        public static QueryConfigurer ForEach<T1>(this QueryConfigurer queryConfigurer, P1<T1> callback)
            where T1 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            queryConfigurer.RegisterExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    callback.Invoke(ref componentT1);
                }
            });
            return queryConfigurer;
        }


        // ------------------------------------------------- //
        // 2 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2>(Entity entity, ref T1 component1, ref T2 component2);

        public static QueryConfigurer ForEach<T1, T2>(this QueryConfigurer queryConfigurer, E<T1, T2> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            queryConfigurer.RegisterExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    callback.Invoke(entity, ref componentT1, ref componentT2);
                }
            });
            return queryConfigurer;
        }

        public delegate void P<T1, T2>(ref T1 component1, ref T2 component2);

        public static QueryConfigurer ForEach<T1, T2>(this QueryConfigurer queryConfigurer, P<T1, T2> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            queryConfigurer.RegisterExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    callback.Invoke(ref componentT1, ref componentT2);
                }
            });
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 3 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3);

        public static QueryConfigurer ForEach<T1, T2, T3>(this QueryConfigurer queryConfigurer, E<T1, T2, T3> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            queryConfigurer.RegisterExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    callback.Invoke(entity, ref componentT1, ref componentT2, ref componentT3);
                }
            });
            return queryConfigurer;
        }

        public delegate void P<T1, T2, T3>(ref T1 component1, ref T2 component2, ref T3 component3);

        public static QueryConfigurer ForEach<T1, T2, T3>(this QueryConfigurer queryConfigurer, P<T1, T2, T3> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            queryConfigurer.RegisterExecutor(() =>
            {
                foreach (var entity in filter)
                {
                    ref var componentT1 = ref stashT1.Get(entity);
                    ref var componentT2 = ref stashT2.Get(entity);
                    ref var componentT3 = ref stashT3.Get(entity);
                    callback.Invoke(ref componentT1, ref componentT2, ref componentT3);
                }
            });
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 4 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3, T4>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4);

        public static QueryConfigurer ForEach<T1, T2, T3, T4>(this QueryConfigurer queryConfigurer, E<T1, T2, T3, T4> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>(), GetRequestedTypeInfo<T4>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            queryConfigurer.RegisterExecutor(() =>
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
            return queryConfigurer;
        }

        public delegate void P<T1, T2, T3, T4>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4);

        public static QueryConfigurer ForEach<T1, T2, T3, T4>(this QueryConfigurer queryConfigurer, P<T1, T2, T3, T4> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>(), GetRequestedTypeInfo<T4>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            queryConfigurer.RegisterExecutor(() =>
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
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 5 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3, T4, T5>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5);

        public static QueryConfigurer ForEach<T1, T2, T3, T4, T5>(this QueryConfigurer queryConfigurer, E<T1, T2, T3, T4, T5> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter,
                    GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>(), GetRequestedTypeInfo<T4>(),
                    GetRequestedTypeInfo<T5>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            var stashT5 = queryConfigurer.World.GetStash<T5>();
            queryConfigurer.RegisterExecutor(() =>
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
            return queryConfigurer;
        }

        public delegate void P<T1, T2, T3, T4, T5>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component, ref T5 component5);

        public static QueryConfigurer ForEach<T1, T2, T3, T4, T5>(this QueryConfigurer queryConfigurer, P<T1, T2, T3, T4, T5> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>(), GetRequestedTypeInfo<T4>(),
                    GetRequestedTypeInfo<T5>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            var stashT5 = queryConfigurer.World.GetStash<T5>();
            queryConfigurer.RegisterExecutor(() =>
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
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 6 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3, T4, T5, T6>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5,
            ref T6 component6);

        public static QueryConfigurer ForEach<T1, T2, T3, T4, T5, T6>(this QueryConfigurer queryConfigurer, E<T1, T2, T3, T4, T5, T6> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter,
                    GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>(), GetRequestedTypeInfo<T4>(),
                    GetRequestedTypeInfo<T5>(), GetRequestedTypeInfo<T6>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            var stashT5 = queryConfigurer.World.GetStash<T5>();
            var stashT6 = queryConfigurer.World.GetStash<T6>();
            queryConfigurer.RegisterExecutor(() =>
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
            return queryConfigurer;
        }

        public delegate void P<T1, T2, T3, T4, T5, T6>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component, ref T5 component5, ref T6 component6);

        public static QueryConfigurer ForEach<T1, T2, T3, T4, T5, T6>(this QueryConfigurer queryConfigurer, P<T1, T2, T3, T4, T5, T6> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>(), GetRequestedTypeInfo<T4>(),
                    GetRequestedTypeInfo<T5>(), GetRequestedTypeInfo<T6>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            var stashT5 = queryConfigurer.World.GetStash<T5>();
            var stashT6 = queryConfigurer.World.GetStash<T6>();
            queryConfigurer.RegisterExecutor(() =>
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
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 7 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3, T4, T5, T6, T7>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5,
            ref T6 component6, ref T7 component7);

        public static QueryConfigurer ForEach<T1, T2, T3, T4, T5, T6, T7>(this QueryConfigurer queryConfigurer, E<T1, T2, T3, T4, T5, T6, T7> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter,
                    GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>(), GetRequestedTypeInfo<T4>(),
                    GetRequestedTypeInfo<T5>(), GetRequestedTypeInfo<T6>(), GetRequestedTypeInfo<T7>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            var stashT5 = queryConfigurer.World.GetStash<T5>();
            var stashT6 = queryConfigurer.World.GetStash<T6>();
            var stashT7 = queryConfigurer.World.GetStash<T7>();
            queryConfigurer.RegisterExecutor(() =>
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
            return queryConfigurer;
        }

        public delegate void P<T1, T2, T3, T4, T5, T6, T7>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component, ref T5 component5, ref T6 component6,
            ref T7 component7);

        public static QueryConfigurer ForEach<T1, T2, T3, T4, T5, T6, T7>(this QueryConfigurer queryConfigurer, P<T1, T2, T3, T4, T5, T6, T7> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter, GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>(), GetRequestedTypeInfo<T4>(),
                    GetRequestedTypeInfo<T5>(), GetRequestedTypeInfo<T6>(), GetRequestedTypeInfo<T7>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            var stashT5 = queryConfigurer.World.GetStash<T5>();
            var stashT6 = queryConfigurer.World.GetStash<T6>();
            var stashT7 = queryConfigurer.World.GetStash<T7>();
            queryConfigurer.RegisterExecutor(() =>
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
            return queryConfigurer;
        }

        // ------------------------------------------------- //
        // 8 parameters
        // ------------------------------------------------- //

        public delegate void E<T1, T2, T3, T4, T5, T6, T7, T8>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5,
            ref T6 component6, ref T7 component7, ref T8 component8);

        public static QueryConfigurer ForEach<T1, T2, T3, T4, T5, T6, T7, T8>(this QueryConfigurer queryConfigurer, E<T1, T2, T3, T4, T5, T6, T7, T8> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter,
                    GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>(), GetRequestedTypeInfo<T4>(),
                    GetRequestedTypeInfo<T5>(), GetRequestedTypeInfo<T6>(), GetRequestedTypeInfo<T7>(), GetRequestedTypeInfo<T8>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            var stashT5 = queryConfigurer.World.GetStash<T5>();
            var stashT6 = queryConfigurer.World.GetStash<T6>();
            var stashT7 = queryConfigurer.World.GetStash<T7>();
            var stashT8 = queryConfigurer.World.GetStash<T8>();
            queryConfigurer.RegisterExecutor(() =>
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
            return queryConfigurer;
        }

        public delegate void P<T1, T2, T3, T4, T5, T6, T7, T8>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component, ref T5 component5, ref T6 component6,
            ref T7 component7, ref T8 component8);

        public static QueryConfigurer ForEach<T1, T2, T3, T4, T5, T6, T7, T8>(this QueryConfigurer queryConfigurer, P<T1, T2, T3, T4, T5, T6, T7, T8> callback)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            var filter = queryConfigurer.Filter;
            if (!queryConfigurer.SkipValidationEnabled)
                ValidateRequest(queryConfigurer, filter,
                    GetRequestedTypeInfo<T1>(), GetRequestedTypeInfo<T2>(), GetRequestedTypeInfo<T3>(), GetRequestedTypeInfo<T4>(),
                    GetRequestedTypeInfo<T5>(), GetRequestedTypeInfo<T6>(), GetRequestedTypeInfo<T7>(), GetRequestedTypeInfo<T8>());

            var stashT1 = queryConfigurer.World.GetStash<T1>();
            var stashT2 = queryConfigurer.World.GetStash<T2>();
            var stashT3 = queryConfigurer.World.GetStash<T3>();
            var stashT4 = queryConfigurer.World.GetStash<T4>();
            var stashT5 = queryConfigurer.World.GetStash<T5>();
            var stashT6 = queryConfigurer.World.GetStash<T6>();
            var stashT7 = queryConfigurer.World.GetStash<T7>();
            var stashT8 = queryConfigurer.World.GetStash<T8>();
            queryConfigurer.RegisterExecutor(() =>
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
            return queryConfigurer;
        }
    }
}