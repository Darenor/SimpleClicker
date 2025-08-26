using System;

namespace Features.BuyFeature.Components
{
    [Serializable]
    public struct BuyUpgradeRequest
    {
        public int BusinessId;
        public int UpgradeIndex;
    }
}
