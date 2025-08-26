using Features.UI.UIBase.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Features.UI.UIBase.Data
{
    public class PlayerBalanceElementView : MonoBehaviour, IUIElementView
    {
        public int Entity { get; set; }
        public EcsWorld World { get; set; }
        public virtual void Init(IEcsSystems ecsSystems)
        {
            World = ecsSystems.GetWorld();
            var ecsPool = World.GetPool<UIElementViewComponent>();
            Entity = World.NewEntity();
            ref var uiPlayerBalanceElementComponent = ref ecsPool.Add(Entity);
            uiPlayerBalanceElementComponent.View = this;
        }
    }
}

