using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Lostbyte.Toolkit.FactSystem
{
    internal class FactSettings : ScriptableObject
    {
        private const string k_SettingsPath = "Assets/FactSettings.asset";

        [SerializeField] private FactDatabase m_Database;

        public FactDatabase Database => m_Database;
        private static FactSettings TryLoad()
        {
            string filter = $"t:{nameof(FactSettings)}";
            string[] guids = AssetDatabase.FindAssets(filter);

            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<FactSettings>(path);
            }
            return null;
        }
        public static FactSettings GetOrCreateSettings()
        {
            var settings = TryLoad();
            if (settings == null)
            {
                settings = CreateInstance<FactSettings>();
                AssetDatabase.CreateAsset(settings, k_SettingsPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }
        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
    }
}