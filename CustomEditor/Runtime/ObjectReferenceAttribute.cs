using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Lostbyte.Toolkit.CustomEditor
{
    public class OfTypeAttribute : CombinedAttribute
    {
        public readonly Type Type;
        public bool AllowSceneObjects;
        public OfTypeAttribute(Type type, bool allowSceneObjects = true) => (Type, AllowSceneObjects) = (type, allowSceneObjects);
#if UNITY_EDITOR
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, Type, AllowSceneObjects);
        }
        public override float? GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
        public override bool DrawDefaultPropertyField() => false;
#endif
    }
}
