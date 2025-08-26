using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.UI.UIBase.Data
{
    [Serializable]
    public class UIElementData
    {
        [SerializeField] private List<UIElement> collection = new();
        private UIElement _defaultElement = new();
        
        public UIElement GetElement(UIElementId id)
        {
            foreach (var element in collection)
            {
                if (id == element.id) return element;
            }
            return _defaultElement;
        }
    }
}