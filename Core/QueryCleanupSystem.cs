namespace Scellecs.Morpeh
{
    /// <summary>
    /// Base class for cleanup systems that run at the end of the frame (LateUpdate order).
    /// </summary>
    public abstract class QueryCleanupSystem : QuerySystem, ICleanupSystem
    {
    }
}
