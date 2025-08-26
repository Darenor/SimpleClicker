using System;
using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Features.UI.UIBase.Systems
{
    [Serializable]
    public class ProgressUpdateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<BusinessComponent> _pool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<BusinessComponent>()
                .End();
            
            _pool = _world.GetPool<BusinessComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var business = ref _pool.Get(entity);
                if (business.IsOwned && business.Level > 0)
                {
                    business.Progress += Time.deltaTime / business.IncomeDelay;
                }
            }
        }
    }
}

