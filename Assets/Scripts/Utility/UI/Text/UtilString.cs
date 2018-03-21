﻿// Author(s): Paul Calande
// Utility functions related to strings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilString : MonoBehaviour
{
    // Converts a number of seconds into a "digital time" string.
    public static string DigitalTime(float rawSeconds)
    {
        int seconds = Mathf.FloorToInt(rawSeconds);
        int secondsOnly = seconds % 60;
        int minutesOnly = seconds / 60;

        string result = "";
        result += AddZeroPadding(minutesOnly, 2);
        result += ":";
        result += AddZeroPadding(secondsOnly, 2);
        return result;
    }

    // Adds any 0s as padding to digital time if necessary.
    private static string AddZeroPadding(int val, int stringLength)
    {
        string result = val.ToString();
        while (result.Length < stringLength)
        {
            result = "0" + result;
        }
        return result;
    }
}