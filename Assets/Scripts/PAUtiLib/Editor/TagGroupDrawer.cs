// Author(s): Paul Calande
// Property drawer for selecting multiple tags.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TagGroup))]
public class TagGroupDrawer : PropertyDrawer
{
    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {
        string[] tagNames = UnityEditorInternal.InternalEditorUtility.tags;
        List<string> myTags = new List<string>();
        BitField tagField = new BitField();

        int arrayLength = prop.FindPropertyRelative("tags.Array.size").intValue;

        // Retrieve existing strings and convert them to a bit field.
        for (int i = 0; i < arrayLength; ++i)
        {
            string newTag = prop.FindPropertyRelative("tags.Array.data[" + i + "]").stringValue;
            myTags.Add(newTag);
            for (int j = 0; j < tagNames.Length; ++j)
            {
                if (newTag == tagNames[j])
                {
                    tagField.SetIndex(j);
                    break;
                }
            }
        }

        tagField.SetInt(EditorGUI.MaskField(pos, "Tags", tagField.GetInt(), tagNames));

        SerializedProperty array = prop.FindPropertyRelative("tags.Array");

        // Clear the property array.
        while (arrayLength > 0)
        {
            array.DeleteArrayElementAtIndex(0);
            --arrayLength;
        }

        // Convert the bit field back to strings.
        for (int i = 0; i < tagNames.Length; ++i)
        {
            if (tagField.IsIndexSet(i))
            {
                array.InsertArrayElementAtIndex(0);
                array.GetArrayElementAtIndex(0).stringValue = tagNames[i];
            }
        }
    }
}