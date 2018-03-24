// Author(s): Paul Calande
// Class for reading inputs from one FixedUpdate step.
// Key releases should be checked AFTER key presses when using this class.
// This is because a key release and a key press can happen on the same FixedUpdate frame.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader
{
    InputData inputData;

    public InputReader(InputData inputData)
    {
        this.inputData = inputData;
    }

    public bool GetKey(KeyCode key)
    {
        return inputData.keysHeld.Contains(key);
    }

    public bool GetKeyDown(KeyCode key)
    {
        return inputData.keysDown.Contains(key);
    }

    public bool GetKeyUp(KeyCode key)
    {
        return inputData.keysUp.Contains(key);
    }

    // Returns a key from the keys down collection.
    // Returns null if no keys have been pressed down.
    public KeyCode? GetOneKeyDown()
    {
        foreach (KeyCode key in inputData.keysDown)
        {
            return key;
        }
        return null;
    }

    public float GetAxisHorizontal()
    {
        return inputData.axisHorizontal;
    }

    public float GetAxisVertical()
    {
        return inputData.axisVertical;
    }

    public float GetAxisHorizontalRaw()
    {
        return inputData.axisHorizontalRaw;
    }

    public float GetAxisVerticalRaw()
    {
        return inputData.axisVerticalRaw;
    }
}