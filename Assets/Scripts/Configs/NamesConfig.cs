using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "NamesConfig", menuName = "Configs/NameConfig")]
    public class NamesConfig : ScriptableObject
    {
        [System.Serializable]
        public class NameEntry
        {
            public string Key;
            public string Value;
        }

        public NameEntry[] Entries;

        public string GetName(string key)
        {
            foreach (var entry in Entries)
            {
                if (entry.Key == key)
                {
                    return entry.Value;
                }
            }
            return "Unknown";
        }
    }
}