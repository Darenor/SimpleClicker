using System;


namespace Components
{
    [Serializable]
    public struct BusinessComponent
    {
        public int Id;
        public bool IsOwned;
        public int Level;
        public bool Upgrade1Bought;
        public bool Upgrade2Bought;
        public float Progress;
        public float CurrentIncome;
        public float BaseIncome;
        public float BaseCost;
        public float IncomeDelay;
        public float Upgrade1Multiplier;
        public float Upgrade2Multiplier;
        public float Upgrade1Cost;
        public float Upgrade2Cost;
        public string BaseNameKey;
        public string Upgrade1NameKey;
        public string Upgrade2NameKey;
    }
}