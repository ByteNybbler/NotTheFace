// Author(s): Paul Calande
// Tracks an integer and updates it accordingly in a Text component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIValueTextInt : UIValueText<int>
{
    public void AddValue(int val)
    {
        SetValue(value + val);
    }

    public void SubtractValue(int val)
    {
        SetValue(value - val);
    }
}