using System;
using Components;
using Leopotam.EcsLite;

namespace Features.Income.Systems
{
    [Serializable]
    public class IncomeCollectionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _businessFilter;
        private EcsFilter _balanceFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _businessFilter = _world
                .Filter<BusinessComponent>()
                .End();

            _balanceFilter = _world
                .Filter<PlayerBalanceComponent>()
                .End();

        }

        public void Run(IEcsSystems systems)
        {
            var businessPool = _world.GetPool<BusinessComponent>();
            var balancePool = _world.GetPool<PlayerBalanceComponent>();

            foreach (var balanceEntity in _balanceFilter)
            {
                ref var balance = ref balancePool.Get(balanceEntity);

                foreach (var businessEntity in _businessFilter)
                {
                    ref var business = ref businessPool.Get(businessEntity);
                    while (business.Progress >= 1f)
                    {
                        balance.Balance += business.CurrentIncome;
                        business.Progress -= 1f;
                    }
                }
            }
        }
    }
}
