using Components;
using System;
using Features.Income.Components;
using Features.Load.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Features.Load.Systems
{
    [Serializable]
    public class LoadSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _balanceFilter;
        private EcsFilter _businessFilter;
        private EcsFilter _requestLoadFilter;
        
        private EcsPool<PlayerBalanceComponent> _balancePool;
        private EcsPool<BusinessComponent> _businessPool;
        private EcsPool<UpdateIncomeRequest> _requestUpdatePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _balanceFilter = _world
                .Filter<PlayerBalanceComponent>()
                .End();
            _businessFilter = _world
                .Filter<BusinessComponent>()
                .End();
            _requestLoadFilter = _world
                .Filter<LoadRequest>()
                .End();
            
            _balancePool = _world.GetPool<PlayerBalanceComponent>();
            _businessPool = _world.GetPool<BusinessComponent>();
            _requestUpdatePool = _world.GetPool<UpdateIncomeRequest>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var requestLoadEntity in _requestLoadFilter)
            {
                LoadGame();
                _world.DelEntity(requestLoadEntity);
            }
        }

        private void LoadGame()
        {
            if (PlayerPrefs.HasKey("PlayerBalance"))
            {
                foreach (var entity in _balanceFilter)
                {
                    ref var balance = ref _balancePool.Get(entity);
                    balance.Balance = PlayerPrefs.GetFloat("PlayerBalance");
                }
                
                foreach (var entity in _businessFilter)
                {
                    var requestUpdateEntity = _world.NewEntity();
                    ref var business = ref _businessPool.Get(entity);
                    var prefix = $"Business_{business.Id}_";
                    business.Level = PlayerPrefs.GetInt(prefix + "Level", business.Level);
                    business.Progress = PlayerPrefs.GetFloat(prefix + "Progress", business.Progress);
                    business.Upgrade1Bought = PlayerPrefs.GetInt(prefix + "Upgrade1Bought", 0) == 1;
                    business.Upgrade2Bought = PlayerPrefs.GetInt(prefix + "Upgrade2Bought", 0) == 1;
                    business.IsOwned = PlayerPrefs.GetInt(prefix + "IsOwned", business.IsOwned ? 1 : 0) == 1;
                    _requestUpdatePool.Add(requestUpdateEntity);
                }
            }
        }
    }
}

