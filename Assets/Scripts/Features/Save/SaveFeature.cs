using Features.Save.Systems;
using LeoCore;
using Leopotam.EcsLite;
using UnityEngine;

namespace Features.Save
{
    
    [CreateAssetMenu(menuName = "Leo Features/Save Feature")]
    public class SaveFeature : BaseEcsFeature
    {
        public override void Init(EcsSystems systems)
        {
            systems.Add(new SaveSystem());
        }
    }
}