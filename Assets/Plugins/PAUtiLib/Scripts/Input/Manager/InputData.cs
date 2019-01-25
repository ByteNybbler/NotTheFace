// Author(s): Paul Calande
// A class built to contain input data for one FixedUpdate step.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData
{
    public float axisHorizontal = 0.0f;
    public float axisVertical = 0.0f;
    public float axisHorizontalRaw = 0.0f;
    public float axisVerticalRaw = 0.0f;
    public HashSet<KeyCode> keysDown = new HashSet<KeyCode>();
    public HashSet<KeyCode> keysUp = new HashSet<KeyCode>();
    public HashSet<KeyCode> keysHeld = new HashSet<KeyCode>();

    // Resets input data.
    public void Clear()
    {
        axisHorizontal = 0.0f;
        axisVertical = 0.0f;
        axisHorizontalRaw = 0.0f;
        axisVerticalRaw = 0.0f;
        keysDown.Clear();
        keysUp.Clear();
        keysHeld.Clear();
    }
}