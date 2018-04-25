// Author(s): Paul Calande
// Lerps an accessor value based on a timer.

using UnityEngine;

public class ColorAccessorLerp : SingleAccessorLerp
    <Color, ColorAccessor>
{
    protected override Color Lerp(Color start, Color end, float normalizedTime)
    {
        return Color.Lerp(start, end, normalizedTime);
    }
}