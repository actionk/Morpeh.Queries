using System;

namespace Scellecs.Morpeh
{
    public interface IQuerySystem
    {
        public World World { get; set; }
        
        internal void AddExecutor(Action newQueryExecutor);
        
#if MORPEH_BURST
        internal void AddJobHandle(QueryBuilderJobHandle jobHandle);
#endif
    }
}