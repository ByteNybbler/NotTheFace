// Author(s): Paul Calande
// Utility functions for formatting text.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilTextFormat
{
    // Add newline ('\n') characters to a string by replacing certain spaces.
    // This creates word wrap. May not work properly if newline characters are
    // already present in the passed string.
    public static string AddWordWrap(string strToWrap,
        Font font, FontStyle fontStyle, int maxWidth, int textSize)
    {
        CharacterInfo charinfo = new CharacterInfo();
        string result = "";
        int widthSoFar = 0;
        int indexOfLatestSpace = -1;
        int widthToLatestSpace = 0;

        // Request that the font texture be updated if necessary.
        // This helps prevent Font.GetCharacterInfo from returning false.
        font.RequestCharactersInTexture(strToWrap, textSize, fontStyle);

        for (int i = 0; i < strToWrap.Length; ++i)
        {
            char c = strToWrap[i];
            if (font.GetCharacterInfo(c, out charinfo, textSize, fontStyle))
            {
                int glyphSize = charinfo.advance;
                widthSoFar += glyphSize;
                if (c == ' ')
                {
                    //Debug.Log("Space found at position " + i);
                    indexOfLatestSpace = i;
                    widthToLatestSpace = widthSoFar;
                }
                else if (widthSoFar >= maxWidth)
                {
                    widthSoFar -= widthToLatestSpace;

                    // Replace the latest space with a line break.
                    //Debug.Log("Current pos for next space operation: " + i);
                    //Debug.Log("Replacing space at " + indexOfLatestSpace + ": " + result);
                    result = result.Remove(indexOfLatestSpace, 1);
                    result = result.Insert(indexOfLatestSpace, "\n");
                    //Debug.Log("Replaced space at " + indexOfLatestSpace + ": " + result);
                }
                //Debug.Log("char / widthSoFar: " + c + " / " + widthSoFar);
            }
            else
            {
                Debug.LogError("AddWordWrap: font.GetCharacterInfo failed (returned false)."
                    + " Character: " + c);
            }

            result += c;
        }

        return result;
    }

    // Add word wrap to a string based on a Text component and its RectTransform.
    // Not to be used with Text's "Best Fit" option.
    public static string AddWordWrap(string strToWrap,
        Text text, RectTransform textRectTransform)
    {
        Font font = text.font;
        FontStyle fontStyle = text.fontStyle;
        int maxWidth = Mathf.FloorToInt(textRectTransform.rect.width);
        int textSize = text.fontSize;
        /*
        Debug.Log("AddWordWrap: font: " + font);
        Debug.Log("AddWordWrap: fontStyle: " + fontStyle);
        Debug.Log("AddWordWrap: maxWidth: " + maxWidth);
        Debug.Log("AddWordWrap: textSize: " + textSize);
        */
        return AddWordWrap(strToWrap, font, fontStyle, maxWidth, textSize);
    }

    // Reverses the order of digits of each number in a string.
    // Used for fixing the order of digits for right-to-left text.
    // Might not work properly for strings that have newline characters.
    private static string FixRightToLeftNumbers(string str)
    {
        string[] strs = str.Split(' ');

        // Check if each "word" is a number, and if so, reverse its order.
        int stringCount = strs.Length;
        for (int i = 0; i < stringCount; ++i)
        {
            if (strs[i].Length != 0)
            {
                if (char.IsNumber(strs[i][0]))
                {
                    strs[i] = UtilGrapheme.ReverseGraphemeClusters(strs[i]);
                }
            }
        }

        return string.Join(" ", strs);
    }

    // Make left-to-right word-wrapped text into right-to-left text.
    public static string MakeWrappedStringRightToLeft(string strWrapped)
    {
        string[] strs = strWrapped.Split('\n');

        // Reverse each string.
        int stringCount = strs.Length;
        for (int i = 0; i < stringCount; ++i)
        {
            strs[i] = UtilGrapheme.ReverseGraphemeClusters(strs[i]);
            strs[i] = FixRightToLeftNumbers(strs[i]);
        }

        return string.Join("\n", strs);
    }

    // Get the right-to-left equivalent of a left-to-right TextAnchor, or vice versa.
    public static TextAnchor GetAnchorInverseHorizontal(TextAnchor ta)
    {
        switch (ta)
        {
            case TextAnchor.LowerLeft:
                return TextAnchor.LowerRight;
            case TextAnchor.LowerRight:
                return TextAnchor.LowerLeft;
            case TextAnchor.MiddleLeft:
                return TextAnchor.MiddleRight;
            case TextAnchor.MiddleRight:
                return TextAnchor.MiddleLeft;
            case TextAnchor.UpperLeft:
                return TextAnchor.UpperRight;
            case TextAnchor.UpperRight:
                return TextAnchor.UpperLeft;
            default:
                // These are the center cases. No need to change anything.
                return ta;
        }
    }

    public static void SetAnchorInverseHorizontal(Text text)
    {
        text.alignment = GetAnchorInverseHorizontal(text.alignment);
    }
}