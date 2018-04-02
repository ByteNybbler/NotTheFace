// Author(s): Paul Calande
// Text formatter script that provides support for both left-to-right and
// right-to-left languages, as well as word wrap.
// Do not use with "Best Fit" enabled on Text component!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRTLSupport : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the Text component to perform formatting on.")]
    Text text;
    [SerializeField]
    [Tooltip("Reference to the text's RectTransform component.")]
    RectTransform textRectTransform;
    [SerializeField]
    [Tooltip("Whether to make the text wrap when it reaches the edge of the RectTransform.")]
    bool useWordWrap;

    // Whether the text anchor has been inverted yet.
    // This makes sure the inversion only occurs the first time the text is formatted.
    bool hasInvertedAnchor = false;

    public void FormatText(string toFormat, bool rightToLeft)
    {
        string result = toFormat;
        if (useWordWrap)
        {
            //Debug.Log("Before: " + result);
            result = UtilTextFormat.AddWordWrap(result, text, textRectTransform);
            //Debug.Log("After: " + result);
        }
        if (rightToLeft)
        {
            result = UtilTextFormat.MakeWrappedStringRightToLeft(result);
            if (!hasInvertedAnchor)
            {
                UtilTextFormat.SetAnchorInverseHorizontal(text);
                hasInvertedAnchor = true;
            }
        }
        text.text = result;
    }
}