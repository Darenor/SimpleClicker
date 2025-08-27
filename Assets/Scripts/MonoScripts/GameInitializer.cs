using Configs;
using Features.UI.UIBase.Components;
using Features.UI.UIBase.Data;
using LeoCore;
using Leopotam.EcsLite;
using UnityEngine;

namespace MonoScripts
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private BusinessConfig businessConfig;
        [SerializeField] private EcsFeatures ecsFeatures;
        [SerializeField] private Transform businessGroupTransform;
        [SerializeField] private Transform balanceBgTransform;

        private EcsSystems _systems;

        private void Start()
        {
            var world = new EcsWorld();
            _systems = new EcsSystems(world);

            ecsFeatures.InitFeatures(_systems);
            SpawnUI(_systems);
            _systems?.Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems?.GetWorld().Destroy();
        }

        private void SpawnUI(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var requestPool = world.GetPool<SpawnElementRequest>();
            
            SpawnBalanceUI(requestPool, world.NewEntity());
            for (var i = 0; i < businessConfig.Businesses.Length; i++)
            {
                SpawnBusinessUI(requestPool, world.NewEntity());
            }
        }

        private void SpawnBalanceUI(EcsPool<SpawnElementRequest> ecsPool, int entity)
        {
            ref var balanceElementRequest = ref ecsPool.Add(entity);
            balanceElementRequest.Transform = balanceBgTransform;
            balanceElementRequest.Element = UIElementId.PlayerBalance;
        }
        private void SpawnBusinessUI(EcsPool<SpawnElementRequest> ecsPool, int entity)
        {
            ref var spawnElementRequest = ref ecsPool.Add(entity);
            spawnElementRequest.Transform = businessGroupTransform;
            spawnElementRequest.Element = UIElementId.BusinessScreen;
        }
    }
}
