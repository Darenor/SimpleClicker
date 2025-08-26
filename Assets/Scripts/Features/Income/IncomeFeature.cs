using Features.Income.Systems;
using LeoCore;
using Leopotam.EcsLite;
using UnityEngine;

namespace Features.Income
{
    [CreateAssetMenu(menuName = "Leo Features/Income Feature")]
    public class IncomeFeature : BaseEcsFeature
    {
        public override void Init(EcsSystems systems)
        {
            systems.Add(new IncomeCollectionSystem());
            systems.Add(new UpdateIncomeSystem());
        }
    }
}

