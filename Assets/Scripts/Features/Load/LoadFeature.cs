using Features.Load.Systems;
using Leopotam.EcsLite;

namespace Features.Load
{
    using LeoCore;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Leo Features/Load Feature")]
    public class LoadFeature : BaseEcsFeature
    {
        public override void Init(EcsSystems systems)
        {
            systems.Add(new LoadSystem());
        }
    }
}

