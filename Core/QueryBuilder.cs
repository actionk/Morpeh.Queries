using System.Reflection;

namespace Scellecs.Morpeh
{
    public class QueryBuilder
    {
        private static readonly MethodInfo FILTER_WITH_METHOD_INFO = typeof(FilterExtensions).GetMethod("With");
        private static readonly MethodInfo FILTER_WITHOUT_METHOD_INFO = typeof(FilterExtensions).GetMethod("Without");

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
                        filterBuilder = (FilterBuilder)FILTER_WITH_METHOD_INFO.MakeGenericMethod(QueryBuilderGlobals.TYPES_TO_REQUIRE[i]).Invoke(filterBuilder, new object[] { filterBuilder });
                }

                if (QueryBuilderGlobals.TYPES_TO_IGNORE.Count > 0)
                {
                    for (var i = 0; i < QueryBuilderGlobals.TYPES_TO_IGNORE.Count; i++)
                        filterBuilder = (FilterBuilder)FILTER_WITHOUT_METHOD_INFO.MakeGenericMethod(QueryBuilderGlobals.TYPES_TO_IGNORE[i]).Invoke(filterBuilder, new object[] { filterBuilder });
                }
            }

            return new CompiledQuery(filterBuilder.Build());
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