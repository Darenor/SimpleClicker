using Leopotam.EcsLite;

namespace Features.UI.UIBase.Data
{
    public interface IUIElementView
    {
        public int Entity { get; set; }
        public EcsWorld World { get; set; }
        public void Init(IEcsSystems ecsSystems);
    }
}