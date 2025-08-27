using Configs;
using Features.UI.Business.Systems;
using LeoCore;
using Leopotam.EcsLite;
using UnityEngine;

namespace Features.UI.Business
{
    
    [CreateAssetMenu(menuName = "Leo Features/UI/Business Feature")]
    public class BusinessFeature : BaseEcsFeature
    {
        [SerializeField] private BusinessConfig businessConfig;
        public override void Init(EcsSystems systems)
        {
            systems.Add(new GetBusinessInfoSystem(businessConfig));
            systems.Add(new UpdateUISystem());
        }
    }
}