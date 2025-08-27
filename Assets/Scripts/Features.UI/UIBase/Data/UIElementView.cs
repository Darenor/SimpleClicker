using Features.UI.UIBase.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Features.UI.UIBase.Data
{
    public class UIElementView : MonoBehaviour, IUIElementView
    {
        public int Entity { get; set; }
        public EcsWorld World { get; set; }
        public virtual void Init(IEcsSystems ecsSystems)
        {
            World = ecsSystems.GetWorld();
            var ecsPool = World.GetPool<UIElementViewComponent>();
            Entity = World.NewEntity();
            ref var uiElementViewComponent = ref ecsPool.Add(Entity);
            uiElementViewComponent.View = this;
        }
    }
}