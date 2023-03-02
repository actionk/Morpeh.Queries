using System.Reflection;

namespace Scellecs.Morpeh
{
    public class QueryBuilder
    {
        private static readonly MethodInfo FILTER_WITH_METHOD_INFO = typeof(FilterExtensions).GetMethod("With");
        private static readonly MethodInfo FILTER_WITHOUT_METHOD_INFO = typeof(FilterExtensions).GetMethod("Without");

        private readonly IQuerySystem m_querySystem;
        internal Filter filter;

        public World World => m_querySystem.World;
        public IQuerySystem System => m_querySystem;
        internal bool skipValidationEnabled;
        internal bool ignoreGlobalsEnabled;

        public QueryBuilder(IQuerySystem querySystem)
        {
            m_querySystem = querySystem;
            filter = querySystem.World.Filter;
        }

        public Filter Build()
        {
            if (!ignoreGlobalsEnabled)
            {
                if (QueryBuilderGlobals.TYPES_TO_REQUIRE.length > 0)
                {
                    for (var i = 0; i < QueryBuilderGlobals.TYPES_TO_REQUIRE.length; i++)
                        filter = (Filter)FILTER_WITH_METHOD_INFO.MakeGenericMethod(QueryBuilderGlobals.TYPES_TO_REQUIRE.data[i]).Invoke(filter, new object[] { filter });
                }

                if (QueryBuilderGlobals.TYPES_TO_IGNORE.length > 0)
                {
                    for (var i = 0; i < QueryBuilderGlobals.TYPES_TO_IGNORE.length; i++)
                        filter = (Filter)FILTER_WITHOUT_METHOD_INFO.MakeGenericMethod(QueryBuilderGlobals.TYPES_TO_IGNORE.data[i]).Invoke(filter, new object[] { filter });
                }
            }

            return filter;
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