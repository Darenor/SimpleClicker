using UnityEngine;

namespace Features.UI.UIBase.Data
{
    [CreateAssetMenu(menuName = "UI/UI Element Map")]
    public class UIElementMap : ScriptableObject
    {
        public UIElementData value = new();
    }
}