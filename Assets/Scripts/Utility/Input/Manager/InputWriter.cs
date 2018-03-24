// Author(s): Paul Calande
// Class for recording inputs leading up to each FixedUpdate step.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWriter
{
    InputData inputData;

    public InputWriter(InputData inputData)
    {
        this.inputData = inputData;
    }

    // Track the keypress data that is recorded each Update step.
    // To be called during Update.
    public void PopulateKeys()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(key))
            {
                inputData.keysHeld.Add(key);
            }
            if (Input.GetKeyDown(key))
            {
                inputData.keysDown.Add(key);
            }
            if (Input.GetKeyUp(key))
            {
                inputData.keysUp.Add(key);
            }
        }
    }

    // Track the axis data.
    public void PopulateAxes()
    {
        inputData.axisHorizontal = Input.GetAxis("Horizontal");
        inputData.axisVertical = Input.GetAxis("Vertical");
        inputData.axisHorizontalRaw = Input.GetAxisRaw("Horizontal");
        inputData.axisVerticalRaw = Input.GetAxisRaw("Vertical");
    }

    // Resets input data for the next FixedUpdate step.
    // To be called after the input data is sent out.
    public void Clear()
    {
        inputData.Clear();
    }
}