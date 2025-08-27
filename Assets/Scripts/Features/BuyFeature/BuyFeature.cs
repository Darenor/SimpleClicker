using Features.BuyFeature.Systems;
using LeoCore;
using Leopotam.EcsLite;
using UnityEngine;

namespace Features.BuyFeature
{

    [CreateAssetMenu(menuName = "Leo Features/Buy Feature")]
    public class BuyFeature : BaseEcsFeature
    {
        public override void Init(EcsSystems systems)
        {
            systems.Add(new BuyLevelSystem());
            systems.Add(new BuyUpgradeSystem());
        }
    }
}

