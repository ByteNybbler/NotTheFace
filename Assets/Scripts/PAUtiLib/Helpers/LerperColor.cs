// Author(s): Paul Calande
// Interpolates between one color and another over a given quantity of time.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerperColor
{
    Lerper<Color> lerper;

    public LerperColor(Color colorStart, Color colorEnd, float secondsToLerp,
        Lerper<Color>.FinishedHandler Finished = null)
    {
        lerper = new Lerper<Color>(Color.Lerp, colorStart, colorEnd, secondsToLerp, Finished);
    }

    public Color Sample(float deltaTime)
    {
        return lerper.Sample(deltaTime);
    }
}