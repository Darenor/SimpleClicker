using System;
using Components;
using Features.BuyFeature.Components;
using Features.Income.Components;
using Leopotam.EcsLite;

namespace Features.BuyFeature.Systems
{
    [Serializable]
    public class BuyLevelSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _requestFilter;
        private EcsFilter _businessFilter;
        private EcsFilter _balanceFilter;
        private EcsPool<BuyLevelRequest> _requestPool;
        private EcsPool<BusinessComponent> _businessPool;
        private EcsPool<PlayerBalanceComponent> _balancePool;
        private EcsPool<UpdateIncomeRequest> _requestUpdatePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _requestFilter = _world
                .Filter<BuyLevelRequest>()
                .End();
            _businessFilter = _world
                .Filter<BusinessComponent>()
                .End();
            _balanceFilter = _world
                .Filter<PlayerBalanceComponent>()
                .End();
            
            _requestPool = _world.GetPool<BuyLevelRequest>();
            _businessPool = _world.GetPool<BusinessComponent>();
            _balancePool = _world.GetPool<PlayerBalanceComponent>();
            _requestUpdatePool = _world.GetPool<UpdateIncomeRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFilter)
            {
                var requestUpdateEntity = _world.NewEntity();
                ref var request = ref _requestPool.Get(requestEntity);
                foreach (var balanceEntity in _balanceFilter)
                {
                    ref var balance = ref _balancePool.Get(balanceEntity);

                    foreach (var businessEntity in _businessFilter)
                    {
                        ref var business = ref _businessPool.Get(businessEntity);
                        if (business.Id == request.BusinessId)
                        {
                            var cost = (business.Level + 1) * business.BaseCost;
                            if (balance.Balance >= cost)
                            {
                                balance.Balance -= cost;
                                business.Level++;
                                if (!business.IsOwned)
                                {
                                    business.IsOwned = true;
                                }
                                _requestUpdatePool.Add(requestUpdateEntity);
                            }
                            break;
                        }
                    }
                }
                _world.DelEntity(requestEntity);
            }
        }
    }
}
