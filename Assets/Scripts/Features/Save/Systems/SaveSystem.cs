using Components;
using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Features.Save.Systems
{
    [Serializable]
    public class SaveSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter _balanceFilter;
        private EcsFilter _businessFilter;
        private EcsPool<PlayerBalanceComponent> _balancePool;
        private EcsPool<BusinessComponent> _businessPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _balancePool = _world.GetPool<PlayerBalanceComponent>();
            _businessPool = _world.GetPool<BusinessComponent>();
            
            _balanceFilter = _world
                .Filter<PlayerBalanceComponent>()
                .End();
            _businessFilter = _world
                .Filter<BusinessComponent>()
                .End();
        }

        private void SaveGame()
        {
            
            foreach (var entity in _balanceFilter)
            {
                ref var balance = ref _balancePool.Get(entity);
                PlayerPrefs.SetFloat("PlayerBalance", balance.Balance);
            }
            
            foreach (var entity in _businessFilter)
            {
                ref var business = ref _businessPool.Get(entity);
                var prefix = $"Business_{business.Id.ToString()}_";
                PlayerPrefs.SetInt(prefix + "Level", business.Level);
                PlayerPrefs.SetFloat(prefix + "Progress", business.Progress);
                PlayerPrefs.SetInt(prefix + "Upgrade1Bought", business.Upgrade1Bought ? 1 : 0);
                PlayerPrefs.SetInt(prefix + "Upgrade2Bought", business.Upgrade2Bought ? 1 : 0);
                PlayerPrefs.SetInt(prefix + "IsOwned", business.IsOwned ? 1 : 0);
            }

            PlayerPrefs.Save();
        }
        
        public void Destroy(IEcsSystems systems)
        {
            SaveGame();
        }
    }
}

