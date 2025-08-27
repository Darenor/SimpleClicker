using System;
using Components;
using Features.UI.Business.Components;
using Leopotam.EcsLite;

namespace Features.UI.Business.Systems
{
    [Serializable]
    public class UpdateUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter _balanceFilter;
        private EcsFilter _businessFilter;
        private EcsFilter _businessViewFilter;

        private EcsPool<PlayerBalanceComponent> _balancePool;
        private EcsPool<BalanceViewComponent> _balanceViewPool;

        private EcsPool<BusinessComponent> _businessPool;
        private EcsPool<BusinessViewComponent> _businessViewPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _balanceFilter = _world
                .Filter<PlayerBalanceComponent>()
                .Inc<BalanceViewComponent>()
                .End();
            _businessFilter = _world
                .Filter<BusinessComponent>()
                .End();
            _businessViewFilter = _world
                .Filter<BusinessViewComponent>()
                .End();

            _balancePool = _world.GetPool<PlayerBalanceComponent>();
            _balanceViewPool = _world.GetPool<BalanceViewComponent>();
            _businessPool = _world.GetPool<BusinessComponent>();
            _businessViewPool = _world.GetPool<BusinessViewComponent>();
        }

        public void Run(IEcsSystems systems)
        {
                foreach (var balanceEntity in _balanceFilter)
                {
                    ref var balance = ref _balancePool.Get(balanceEntity);
                    ref var view = ref _balanceViewPool.Get(balanceEntity);
                    view.PlayerBalanceUI.UpdateBalance(balance.Balance);
                }

                foreach (var businessEntity in _businessFilter)
                {
                    ref var business = ref _businessPool.Get(businessEntity);

                    foreach (var viewEntity in _businessViewFilter)
                    {
                        ref var view = ref _businessViewPool.Get(viewEntity);
                        var levelUpCost = (business.Level + 1) * business.BaseCost;

                        if (business.Id == view.Id)
                        {
                            view.BusinessUI.UpdateUI(
                                business.BaseNameKey,
                                business.IsOwned,
                                business.Level,
                                business.CurrentIncome,
                                business.Progress,
                                levelUpCost,
                                business.Upgrade1Bought,
                                business.Upgrade2Bought,
                                business.Upgrade1NameKey,
                                business.Upgrade2NameKey,
                                business.Upgrade1Cost,
                                business.Upgrade2Cost,
                                business.Upgrade1Multiplier,
                                business.Upgrade2Multiplier);
                        }
                    }
                }
        }
    }
}