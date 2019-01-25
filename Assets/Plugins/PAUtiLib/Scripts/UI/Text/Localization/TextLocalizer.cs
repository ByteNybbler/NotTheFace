// Author(s): Paul Calande
// Localizes text based on a given translation key.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLocalizer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The translation key to fetch from the translation files.")]
    string translationKey;
    [SerializeField]
    [Tooltip("The component to use to format the text.")]
    TextRTLSupport textFormatter;

    private void Start()
    {
        Translator trans = ServiceLocator.GetTranslator();
        textFormatter.FormatText(trans.Translate(translationKey), trans.IsLanguageRTL());
    }
}