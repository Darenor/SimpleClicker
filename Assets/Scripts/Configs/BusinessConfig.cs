using System;

namespace Configs
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "BusinessConfig", menuName = "Configs/BusinessConfig")]
    public class BusinessConfig : ScriptableObject
    {
        public BusinessData[] Businesses = new BusinessData[5];
    }
}
