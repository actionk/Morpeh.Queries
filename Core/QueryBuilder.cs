using System;
using System.Collections.Generic;
using System.Reflection;

namespace Scellecs.Morpeh
{
    public class QueryBuilder
    {
        private static readonly MethodInfo FILTER_WITH_METHOD_INFO = typeof(FilterBuilder).GetMethod("With");
        private static readonly MethodInfo FILTER_WITHOUT_METHOD_INFO = typeof(FilterBuilder).GetMethod("Without");

        // Cache closed-generic MethodInfos to avoid MakeGenericMethod allocations on every Build() call
        private static readonly Dictionary<Type, MethodInfo> s_withMethodCache = new();
        private static readonly Dictionary<Type, MethodInfo> s_withoutMethodCache = new();

        private readonly IQuerySystem m_querySystem;
        internal FilterBuilder filterBuilder;

        public World World => m_querySystem.World;
        public IQuerySystem System => m_querySystem;
        internal bool skipValidationEnabled;
        internal bool ignoreGlobalsEnabled;

        public QueryBuilder(IQuerySystem querySystem)
        {
            m_querySystem = querySystem;
            filterBuilder = querySystem.World.Filter;
        }

        public CompiledQuery Build()
        {
            if (!ignoreGlobalsEnabled)
            {
                if (QueryBuilderGlobals.TYPES_TO_REQUIRE.Count > 0)
                {
                    for (var i = 0; i < QueryBuilderGlobals.TYPES_TO_REQUIRE.Count; i++)
                    {
                        var type = QueryBuilderGlobals.TYPES_TO_REQUIRE[i];
                        if (!s_withMethodCache.TryGetValue(type, out var method))
                            s_withMethodCache[type] = method = FILTER_WITH_METHOD_INFO.MakeGenericMethod(type);
                        filterBuilder = (FilterBuilder)method.Invoke(filterBuilder, null);
                    }
                }

                if (QueryBuilderGlobals.TYPES_TO_IGNORE.Count > 0)
                {
                    for (var i = 0; i < QueryBuilderGlobals.TYPES_TO_IGNORE.Count; i++)
                    {
                        var type = QueryBuilderGlobals.TYPES_TO_IGNORE[i];
                        if (!s_withoutMethodCache.TryGetValue(type, out var method))
                            s_withoutMethodCache[type] = method = FILTER_WITHOUT_METHOD_INFO.MakeGenericMethod(type);
                        filterBuilder = (FilterBuilder)method.Invoke(filterBuilder, null);
                    }
                }
            }

            return new CompiledQuery(filterBuilder.Build());
        }

        /// <summary>
        /// Applies a Morpeh IFilterExtension to this query.
        /// Allows reusing pre-defined filter compositions across multiple queries.
        /// </summary>
        public QueryBuilder Extend<T>() where T : struct, IFilterExtension
        {
            filterBuilder = filterBuilder.Extend<T>();
            return this;
        }

        #region Parameters

        public QueryBuilder SkipValidation(bool skipValidation)
        {
            skipValidationEnabled = skipValidation;
            return this;
        }

        public QueryBuilder IgnoreGlobals(bool ignoreGlobals)
        {
            ignoreGlobalsEnabled = ignoreGlobals;
            return this;
        }

        #endregion
    }
}