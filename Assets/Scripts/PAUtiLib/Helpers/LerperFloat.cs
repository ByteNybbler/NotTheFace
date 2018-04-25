// Author(s): Paul Calande
// Interpolates between one float and another over a given quantity of time.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerperFloat
{
    Lerper<float> lerper;

    public LerperFloat(float start, float end, float secondsToLerp,
        Lerper<float>.FinishedHandler Finished = null)
    {
        lerper = new Lerper<float>(Mathf.Lerp, start, end, secondsToLerp, Finished);
    }

    public float Sample(float deltaTime)
    {
        return lerper.Sample(deltaTime);
    }
}