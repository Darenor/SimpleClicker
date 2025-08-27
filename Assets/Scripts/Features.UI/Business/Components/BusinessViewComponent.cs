using System;
using Features.UI.Business.Views;

namespace Features.UI.Business.Components
{
    [Serializable]
    public struct BusinessViewComponent
    {
        public BusinessUI BusinessUI;
        public int Id;
    }
}