using System;

namespace Scellecs.Morpeh
{
    public class QueryConfigurer
    {
        private readonly QuerySystem m_querySystem;
        
        public Filter Filter { get; private set; }
        public World World => m_querySystem.World;
        public QuerySystem System => m_querySystem;
        public bool SkipValidationEnabled { get; set; }

        public QueryConfigurer(QuerySystem querySystem)
        {
            m_querySystem = querySystem;
            Filter = querySystem.World.Filter;
        }

#region Parameters

        public QueryConfigurer SkipValidation(bool skipValidation)
        {
            SkipValidationEnabled = skipValidation;
            return this;
        }
        
#endregion

#region Alternatives

        public QueryConfigurer Also(Action<Filter> filterCallback)
        {
            filterCallback?.Invoke(Filter);
            return this;
        }

        public QueryConfigurer With<T>() where T : struct, IComponent
        {
            Filter = Filter.With<T>();
            return this;
        }

        public QueryConfigurer Without<T>() where T : struct, IComponent
        {
            Filter = Filter.Without<T>();
            return this;
        }

#endregion

#region WithAll

        public QueryConfigurer WithAll<T1>()
            where T1 : struct, IComponent
        {
            Filter = Filter.With<T1>();
            return this;
        }

        public QueryConfigurer WithAll<T1, T2>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            Filter = Filter.With<T1>().With<T2>();
            return this;
        }

        public QueryConfigurer WithAll<T1, T2, T3>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            Filter = Filter.With<T1>().With<T2>().With<T3>();
            return this;
        }

        public QueryConfigurer WithAll<T1, T2, T3, T4>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            Filter = Filter.With<T1>().With<T2>().With<T3>().With<T4>();
            return this;
        }

        public QueryConfigurer WithAll<T1, T2, T3, T4, T5>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            Filter = Filter.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>();
            return this;
        }

        public QueryConfigurer WithAll<T1, T2, T3, T4, T5, T6>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            Filter = Filter.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>().With<T6>();
            return this;
        }

        public QueryConfigurer WithAll<T1, T2, T3, T4, T5, T6, T7>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            Filter = Filter.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>().With<T6>().With<T7>();
            return this;
        }

        public QueryConfigurer WithAll<T1, T2, T3, T4, T5, T6, T7, T8>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            Filter = Filter.With<T1>().With<T2>().With<T3>().With<T4>().With<T5>().With<T6>().With<T7>().With<T8>();
            return this;
        }

#endregion

#region WithNone

        public QueryConfigurer WithNone<T1>() where T1 : struct, IComponent
        {
            Filter = Filter.Without<T1>();
            return this;
        }

        public QueryConfigurer WithNone<T1, T2>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            Filter = Filter.Without<T1>().Without<T2>();
            return this;
        }

        public QueryConfigurer WithNone<T1, T2, T3>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            Filter = Filter.Without<T1>().Without<T2>().Without<T3>();
            return this;
        }

        public QueryConfigurer WithNone<T1, T2, T3, T4>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            Filter = Filter.Without<T1>().Without<T2>().Without<T3>().Without<T4>();
            return this;
        }

        public QueryConfigurer WithNone<T1, T2, T3, T4, T5>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            Filter = Filter.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>();
            return this;
        }

        public QueryConfigurer WithNone<T1, T2, T3, T4, T5, T6>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            Filter = Filter.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>().Without<T6>();
            return this;
        }

        public QueryConfigurer WithNone<T1, T2, T3, T4, T5, T6, T7>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            Filter = Filter.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>().Without<T6>().Without<T7>();
            return this;
        }

        public QueryConfigurer WithNone<T1, T2, T3, T4, T5, T6, T7, T8>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            Filter = Filter.Without<T1>().Without<T2>().Without<T3>().Without<T4>().Without<T5>().Without<T6>().Without<T7>().Without<T8>();
            return this;
        }

#endregion
    }
}