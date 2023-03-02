using System;
using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;

namespace Plugins.morpeh_plugins.Morpeh.WorldFeatures
{
    public class WorldFeaturesSystem : ICleanupSystem
    {
        public World World { get; set; }

        private readonly Dictionary<Type, IWorldFeature> m_features = new();

        public void OnAwake()
        {
        }

        public void Dispose()
        {
        }

        public void OnUpdate(float deltaTime)
        {
            foreach(var feature in m_features.Values)
                feature.OnCleanupUpdate();
        }

        public void AddFeature<T>(T worldFeature) where T : class, IWorldFeature
        {
            if (!m_features.TryAdd(typeof(T), worldFeature))
            {
                Debug.LogError($"World feature [{typeof(T)}] is already registered in world [{World}]!");
                return;
            }

            worldFeature.Initialize(World);
        }

        public void RemoveFeature<T>() where T : class, IWorldFeature
        {
            if (!m_features.TryGetValue(typeof(T), out IWorldFeature feature))
                return;

            feature.Dispose();
            m_features.Remove(typeof(T));
        }

        public bool TryGetFeature<T>(out T feature) where T : class, IWorldFeature
        {
            feature = m_features.GetValueOrDefault(typeof(T), default) as T;
            return feature != null;
        }
    }
}