using System;
using Components;
using Configs;
using Features.Income.Components;
using Features.Load.Components;
using Features.UI.Business.Components;
using Leopotam.EcsLite;

namespace Features.UI.Business.Systems
{
    [Serializable]
    public class GetBusinessInfoSystem : IEcsInitSystem, IEcsRunSystem
    {
        
        private readonly BusinessConfig _businessConfig;
        private EcsWorld _world;
        private EcsFilter _requestFilter;
        private EcsPool<BusinessComponent> _businessPool;
        private EcsPool<GetBusinessInfoRequest> _requestPool;
        private EcsPool<LoadRequest> _requestLoadPool;


        public GetBusinessInfoSystem(BusinessConfig businessConfig)
        {
            _businessConfig = businessConfig;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _requestFilter = _world
                .Filter<GetBusinessInfoRequest>()
                .End();
            
            _businessPool = _world.GetPool<BusinessComponent>();
            _requestPool = _world.GetPool<GetBusinessInfoRequest>();
            _requestLoadPool = _world.GetPool<LoadRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFilter)
            {
                for (int i = 0; i < _businessConfig.Businesses.Length; i++)
                {
                    var entity = _world.NewEntity();
                    ref var business = ref _businessPool.Add(entity);
                    var data = _businessConfig.Businesses[i];

                    business.Id = i;
                    business.BaseIncome = data.BaseIncome;
                    business.BaseCost = data.BaseCost;
                    business.IncomeDelay = data.IncomeDelay;
                    business.Upgrade1Multiplier = data.Upgrade1.IncomeMultiplier;
                    business.Upgrade2Multiplier = data.Upgrade2.IncomeMultiplier;
                    business.Upgrade1Cost = data.Upgrade1.Cost;
                    business.Upgrade2Cost = data.Upgrade2.Cost;
                    business.BaseNameKey = data.BaseNameKey;
                    business.Upgrade1NameKey = data.Upgrade1.NameKey;
                    business.Upgrade2NameKey = data.Upgrade2.NameKey;
                    business.IsOwned = (i == 0);
                    business.Level = business.IsOwned ? 1 : 0;
                    business.Progress = 0f;
                    business.Upgrade1Bought = false;
                    business.Upgrade2Bought = false;
                }
                _requestPool.Del(requestEntity);
                _requestLoadPool.Add(_world.NewEntity());
            }
        }
    }
}