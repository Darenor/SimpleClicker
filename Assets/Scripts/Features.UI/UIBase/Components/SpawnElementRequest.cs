using System;
using Features.UI.UIBase.Data;
using UnityEngine;

namespace Features.UI.UIBase.Components
{
    [Serializable]
    public struct SpawnElementRequest
    {
        public Transform Transform;
        public UIElementId Element;
    }
}