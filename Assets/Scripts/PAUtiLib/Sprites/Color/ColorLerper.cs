// Author(s): Paul Calande
// Interpolates between one color and another over a given quantity of time.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper
{
    // Invoked when the lerp is finished.
    public delegate void FinishedHandler(float secondsOverflow);
    FinishedHandler Finished;

    // The color at the start of the lerp.
    Color colorStart;
    // The color at the end of the lerp.
    Color colorEnd;
    // How many seconds it takes for the lerp to finish.
    float secondsTarget;
    // The current position in time of the ColorLerper.
    float secondsCurrent = 0.0f;

    public ColorLerper(Color colorStart, Color colorEnd, float secondsToLerp,
        FinishedHandler Finished = null)
    {
        this.colorStart = colorStart;
        this.colorEnd = colorEnd;
        this.secondsTarget = secondsToLerp;
        this.Finished = Finished;
    }

    // Progress by the given amount of time and return the resulting color.
    public Color Sample(float deltaTime)
    {
        secondsCurrent += deltaTime;
        if (secondsCurrent >= secondsTarget)
        {
            OnFinished(secondsCurrent - secondsTarget);
        }
        return Color.Lerp(colorStart, colorEnd, secondsCurrent / secondsTarget);
    }

    private void OnFinished(float secondsOverflow)
    {
        if (Finished != null)
        {
            Finished(secondsOverflow);
        }
    }
}