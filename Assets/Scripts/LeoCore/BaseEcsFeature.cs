using Leopotam.EcsLite;
using UnityEngine;
using LeoCore.Interfaces;

namespace LeoCore
{
    public class BaseEcsFeature : ScriptableObject, IEcsInterface
    {
        public virtual void Init(EcsSystems systems) {}
    }
}
