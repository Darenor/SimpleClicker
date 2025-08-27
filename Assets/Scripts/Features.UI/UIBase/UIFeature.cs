using Features.UI.UIBase.Data;
using Features.UI.UIBase.Systems;
using LeoCore;
using Leopotam.EcsLite;
using UnityEngine;

namespace Features.UI.UIBase
{
    [CreateAssetMenu(menuName = "Leo Features/UI Feature")]
    public class UIFeature : BaseEcsFeature
    {
        [SerializeField] private UIElementMap uiElementMap;
        
        public override void Init(EcsSystems systems)
        {
            var map = Instantiate(uiElementMap);
            systems.Add(new SpawnUIElementsSystem(map.value));
            
            systems.Add(new ProgressUpdateSystem());
        }
    }
}