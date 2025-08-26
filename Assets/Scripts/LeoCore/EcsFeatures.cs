using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace LeoCore
{
    [CreateAssetMenu(menuName = "Ecs Features")]
    public class EcsFeatures : ScriptableObject
    {
        [SerializeField] private BaseEcsFeature[] features = Array.Empty<BaseEcsFeature>();
        public void InitFeatures(EcsSystems systems)
        {
            foreach (var feature in features)
            {
                feature.Init(systems);
            }
        }
    }
}