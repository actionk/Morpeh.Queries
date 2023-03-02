using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using UnityEngine;

namespace Plugins.morpeh_plugins.Morpeh.WorldFeatures
{
    public static class WorldFeatures
    {
#if UNITY_2019_1_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()
        {
            WORLD_TO_FEATURE_SYSTEM_MAP.Clear();
        }
#endif

        private static readonly IntHashMap<WorldFeaturesSystem> WORLD_TO_FEATURE_SYSTEM_MAP = new();
        
        public static void EnableFeature<T>(this World world) where T : class, IWorldFeature, new()
        {
            EnableFeature(world, new T());
        }

        public static void EnableFeature<T>(this World world, T feature) where T : class, IWorldFeature
        {
            if (!WORLD_TO_FEATURE_SYSTEM_MAP.TryGetValue(world.identifier, out WorldFeaturesSystem worldFeaturesSystem))
            {
                worldFeaturesSystem = new WorldFeaturesSystem();
                var systemGroup = world.CreateSystemsGroup();
                systemGroup.AddSystem(worldFeaturesSystem);
                world.AddPluginSystemsGroup(systemGroup);
                WORLD_TO_FEATURE_SYSTEM_MAP.Add(world.identifier, worldFeaturesSystem, out _);
            }

            worldFeaturesSystem!.AddFeature(feature);
        }
        
        public static bool TryGetFeature<T>(this World world, out T feature) where T : class, IWorldFeature
        {
            feature = default;
            if (!WORLD_TO_FEATURE_SYSTEM_MAP.TryGetValue(world.identifier, out WorldFeaturesSystem worldFeaturesSystem))
                return false;

            return worldFeaturesSystem!.TryGetFeature<T>(out feature);
        }

        public static void DisableFeature<T>(this World world) where T : class, IWorldFeature
        {
            if (!WORLD_TO_FEATURE_SYSTEM_MAP.TryGetValue(world.identifier, out WorldFeaturesSystem worldFeaturesSystem))
                return;

            worldFeaturesSystem!.RemoveFeature<T>();
        }
    }
}