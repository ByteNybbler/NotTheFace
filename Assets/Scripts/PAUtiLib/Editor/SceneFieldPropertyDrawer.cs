// Author(s): Unity Answers
// https://answers.unity.com/questions/242794/inspector-field-for-scene-asset.html
// Provides a serializable field for scene assets.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, GUIContent.none, property);
        SerializedProperty sceneAsset = property.FindPropertyRelative("sceneAsset");
        SerializedProperty scenePath = property.FindPropertyRelative("scenePath");
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        if (sceneAsset != null)
        {
            EditorGUI.BeginChangeCheck();
            var value = EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
            if (EditorGUI.EndChangeCheck())
            {
                sceneAsset.objectReferenceValue = value;
                if (sceneAsset.objectReferenceValue != null)
                {
                    string scenePathStr = AssetDatabase.GetAssetPath(sceneAsset.objectReferenceValue);
                    int assetsIndex = scenePathStr.IndexOf("Assets", StringComparison.Ordinal) + 7;
                    int extensionIndex = scenePathStr.LastIndexOf(".unity", StringComparison.Ordinal);
                    scenePathStr = scenePathStr.Substring(assetsIndex, extensionIndex - assetsIndex);
                    scenePathStr = "Assets/" + scenePathStr + ".unity";
                    scenePath.stringValue = scenePathStr;
                }
            }
        }
        EditorGUI.EndProperty();
    }
}