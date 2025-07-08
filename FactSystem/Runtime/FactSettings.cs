using System.Linq;
using UnityEngine;

namespace Lostbyte.Toolkit.FactSystem
{
    internal class FactSettings : ScriptableObject
    {
        [SerializeField] private FactDatabase m_Database;

        public FactDatabase Database => m_Database;
        public static FactSettings TryLoad()
        {
            // string filter = $"t:{nameof(FactSettings)}";
            FactSettings settings = Resources.LoadAll<FactSettings>("").FirstOrDefault();
            return settings;
        }
    }
}