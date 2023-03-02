namespace Scellecs.Morpeh
{
    public interface IWorldFeature
    {
        void Initialize(World world);
        void OnCleanupUpdate();
        void Dispose();
    }
}