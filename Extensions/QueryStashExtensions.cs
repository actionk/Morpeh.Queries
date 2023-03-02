using Scellecs.Morpeh.Collections;

namespace Scellecs.Morpeh
{
    public static class QueryStashExtensions
    {
        public static T GetValue<T>(this Stash<T> stash, Entity entity) where T : struct, IComponent
        {
            return stash.components.GetValueByKey(entity.entityId.id);
        }
    }
}