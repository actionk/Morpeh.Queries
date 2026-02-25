namespace Scellecs.Morpeh
{
    /// <summary>
    /// Base class for systems that run in FixedUpdate.
    /// Inherit from this instead of QuerySystem when registering via group.AddSystem
    /// and you need physics-step timing.
    /// </summary>
    public abstract class QueryFixedSystem : QuerySystem, IFixedSystem
    {
    }
}
