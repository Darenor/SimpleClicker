using System;
using Components;
using Features.Income.Components;
using Leopotam.EcsLite;

namespace Features.Income.Systems
{
    [Serializable]
    public class UpdateIncomeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _businessFilter;
        private EcsFilter _requestFilter;
        private EcsPool<BusinessComponent> _businessPool;
        private EcsPool<UpdateIncomeRequest> _requestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _businessFilter = _world
                .Filter<BusinessComponent>()
                .End();

            _requestFilter = _world
                .Filter<UpdateIncomeRequest>()
                .End();

            _businessPool = _world.GetPool<BusinessComponent>();
            _requestPool = _world.GetPool<UpdateIncomeRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFilter)
            {
                foreach (var businessEntity in _businessFilter)
                {
                    ref var business = ref _businessPool.Get(businessEntity);
                    if(business.Level == 0) continue;
                    var multiplier = 1f + (business.Upgrade1Bought ?business.Upgrade1Multiplier/100 : 0f)
                        + (business.Upgrade2Bought ? business.Upgrade2Multiplier/100 : 0f);
                    business.CurrentIncome = business.Level * business.BaseIncome * multiplier;
                }
                _requestPool.Del(requestEntity);
            }
        }
    }
}

