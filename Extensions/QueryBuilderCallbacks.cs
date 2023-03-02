namespace Scellecs.Morpeh
{
    public static class QueryBuilderCallbacks
    {
        public delegate void E(Entity entity);
        public delegate void EC<T1>(Entity entity, ref T1 component);
        public delegate void C<T1>(ref T1 component);
        
        public delegate void EC<T1, T2>(Entity entity, ref T1 component1, ref T2 component2);
        public delegate void C<T1, T2>(ref T1 component1, ref T2 component2);

        public delegate void EC<T1, T2, T3>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3);
        public delegate void C<T1, T2, T3>(ref T1 component1, ref T2 component2, ref T3 component3);

        public delegate void EC<T1, T2, T3, T4>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4);
        public delegate void C<T1, T2, T3, T4>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4);

        public delegate void EC<T1, T2, T3, T4, T5>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5);
        public delegate void C<T1, T2, T3, T4, T5>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component, ref T5 component5);

        public delegate void EC<T1, T2, T3, T4, T5, T6>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5,
            ref T6 component6);
        public delegate void C<T1, T2, T3, T4, T5, T6>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component, ref T5 component5, ref T6 component6);

        public delegate void EC<T1, T2, T3, T4, T5, T6, T7>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5,
            ref T6 component6, ref T7 component7);
        public delegate void C<T1, T2, T3, T4, T5, T6, T7>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component, ref T5 component5, ref T6 component6,
            ref T7 component7);

        public delegate void EC<T1, T2, T3, T4, T5, T6, T7, T8>(Entity entity, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5,
            ref T6 component6, ref T7 component7, ref T8 component8);
        public delegate void C<T1, T2, T3, T4, T5, T6, T7, T8>(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component, ref T5 component5, ref T6 component6,
            ref T7 component7, ref T8 component8);


    }
}