using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Lostbyte.Toolkit.FactSystem.Editor
{
    static class FactSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateFactSettingsProvider()
        {
            var provider = new SettingsProvider("Project/Facts", SettingsScope.Project)
            {
                label = "Fact System",
                guiHandler = (searchContext) =>
                {
                    var settings = FactSettings.GetOrCreateSettings();
                    var so = new SerializedObject(settings);
                    var dbProp = so.FindProperty("m_Database");
                    EditorGUILayout.PropertyField(dbProp, new GUIContent("Fact Database"));

                    if (dbProp.objectReferenceValue == null && GUILayout.Button("Create Fact Database"))
                    {
                        string assetPath = AssetDatabase.GetAssetPath(settings);
                        string folderPath = Path.GetDirectoryName(assetPath);
                        string path = $"{folderPath}/FactDatabase.asset";
                        var database = ScriptableObject.CreateInstance<FactDatabase>();
                        AssetDatabase.CreateAsset(database, path);
                        AssetDatabase.SaveAssets();
                        dbProp.objectReferenceValue = database;
                    }
                    so.ApplyModifiedPropertiesWithoutUndo();
                },
                keywords = new HashSet<string>(new[] { "Facts", "Fact System", "Game Data" })
            };

            return provider;
        }
    }
}