using System;
using Features.UI.UIBase.Components;
using Features.UI.UIBase.Data;
using Leopotam.EcsLite;
using Object = UnityEngine.Object;

namespace Features.UI.UIBase.Systems
{
    [Serializable]
    public class SpawnUIElementsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _requestFilter;
        private EcsPool<SpawnElementRequest> _requestPool;
        private readonly UIElementData _uiElementData;
        
        public SpawnUIElementsSystem(UIElementData uiElementData)
        {
            _uiElementData = uiElementData;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestFilter = _world
                .Filter<SpawnElementRequest>()
                .End();
            
            _requestPool = _world.GetPool<SpawnElementRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFilter)
            {
                ref var spawnElementRequest = ref _requestPool.Get(requestEntity);
                var uiElement = _uiElementData.GetElement(spawnElementRequest.Element);

                var uiElementView = Object.Instantiate(uiElement.element, spawnElementRequest.Transform);
                uiElementView.Init(systems);
                _requestPool.Del(requestEntity);
            }
        }
    }
}