// Author(s): Paul Calande
// Class for a language.

using UnityEngine;

[System.Serializable]
public class Language
{
    [SerializeField]
    [Tooltip("The file with all of the game's translations for this language.")]
    TextAsset translationsFile;
    [SerializeField]
    [Tooltip("The name of the language as it appears in-game,"
        + " as well as the string used when the language selection is saved.")]
    string name;
    [SerializeField]
    [Tooltip("Whether the language reads from left-to-right or right-to-left.")]
    bool rightToLeft;

    public TextAsset GetTranslationFile()
    {
        return translationsFile;
    }

    public string GetName()
    {
        return name;
    }

    public bool IsRightToLeft()
    {
        return rightToLeft;
    }
}