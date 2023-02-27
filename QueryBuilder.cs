using System;
using System.Reflection;

namespace Scellecs.Morpeh
{
    public class QueryBuilder
    {
        private static readonly MethodInfo FILTER_WITH_METHOD_INFO = typeof(FilterExtensions).GetMethod("With");
        private static readonly MethodInfo FILTER_WITHOUT_METHOD_INFO = typeof(FilterExtensions).GetMethod("Without");
        
        private readonly QuerySystem m_querySystem;
        private Filter m_filter;
        
        public World World => m_querySystem.World;
        public QuerySystem System => m_querySystem;
        internal bool skipValidationEnabled;
        internal bool ignoreGlobalsEnabled;

        public QueryBuilder(QuerySystem querySystem)
        {
            m_querySystem = querySystem;
            m_filter = querySystem.World.Filter;
        }

        public Filter Build()
        {
            if (!ignoreGlobalsEnabled)
            {
                if (QueryBuilderGlobals.TYPES_TO_REQUIRE.length > 0)
                {
                    for (var i = 0; i < QueryBuilderGlobals.TYPES_TO_REQUIRE.length; i++)
                        m_filter = (Filter)FILTER_WITH_METHOD_INFO.MakeGenericMethod(QueryBuilderGlobals.TYPES_TO_REQUIRE.data[i]).Invoke(m_filter, new object[] { m_filter });
                }
                
                if (QueryBuilderGlobals.TYPES_TO_IGNORE.length > 0)
                {
                    for (var i = 0; i < QueryBuilderGlobals.TYPES_TO_IGNORE.length; i++)
                        m_filter = (Filter)FILTER_WITHOUT_METHOD_INFO.MakeGenericMethod(QueryBuilderGlobals.TYPES_TO_IGNORE.data[i]).Invoke(m_filter, new object[] { m_filter });
                }
            }
            
            return m_filter;
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

#region Alternatives

        public QueryBuilder Also(Action<Filter> filterCallback)
        {
            filterCallback?.Invoke(m_filter);
            return this;
        }

        public QueryBuilder With<T>() where T : struct, IComponent
        {
            m_filter = m_filter.With<T>();
            return this;
        }

        public QueryBuilder Without<T>() where T : struct, IComponent
        {
            m_filter = m_filter.Without<T>();
            return this;
        }

#endregion

#region WithAll

        public QueryBuilder WithAll<T1>()
            where T1 : struct, IComponent
        {
            m_filter = m_filter.With<T1>();
            return this;
        }

        public QueryBuilder WithAll<T1, T2>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            m_filter = m_filter.With<T1>().With<T2>();
            return this;
        }

        public QueryBuilder WithAll<T1, T2, T3>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            m_filter = m_filter.With<T1>().With<T2>().With<T3>();
            return this;
        }

        public QueryBuilder WithAll<T1, T2, T3, T4>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            m_filter = m_filter.With<T1>().With<T2>().With<T3>().With<T4>();
            return this;
        }

        public QueryBuilder WithAll<T1, T2, T3, T4, T5>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            m_filter = m_filter.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>();
            return this;
        }

        public QueryBuilder WithAll<T1, T2, T3, T4, T5, T6>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            m_filter = m_filter.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>().With<T6>();
            return this;
        }

        public QueryBuilder WithAll<T1, T2, T3, T4, T5, T6, T7>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            m_filter = m_filter.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>().With<T6>().With<T7>();
            return this;
        }

        public QueryBuilder WithAll<T1, T2, T3, T4, T5, T6, T7, T8>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            m_filter = m_filter.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>().With<T6>().With<T7>().With<T8>();
            return this;
        }

#endregion

#region WithNone

        public QueryBuilder WithNone<T1>() where T1 : struct, IComponent
        {
            m_filter = m_filter.Without<T1>();
            return this;
        }

        public QueryBuilder WithNone<T1, T2>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            m_filter = m_filter.Without<T1>().Without<T2>();
            return this;
        }

        public QueryBuilder WithNone<T1, T2, T3>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            m_filter = m_filter.Without<T1>().Without<T2>().Without<T3>();
            return this;
        }

        public QueryBuilder WithNone<T1, T2, T3, T4>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            m_filter = m_filter.Without<T1>().Without<T2>().Without<T3>().Without<T4>();
            return this;
        }

        public QueryBuilder WithNone<T1, T2, T3, T4, T5>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            m_filter = m_filter.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>();
            return this;
        }

        public QueryBuilder WithNone<T1, T2, T3, T4, T5, T6>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            m_filter = m_filter.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>().Without<T6>();
            return this;
        }

        public QueryBuilder WithNone<T1, T2, T3, T4, T5, T6, T7>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            m_filter = m_filter.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>().Without<T6>().Without<T7>();
            return this;
        }

        public QueryBuilder WithNone<T1, T2, T3, T4, T5, T6, T7, T8>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            m_filter = m_filter.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>().Without<T6>().Without<T7>().Without<T8>();
            return this;
        }

#endregion
    }
}