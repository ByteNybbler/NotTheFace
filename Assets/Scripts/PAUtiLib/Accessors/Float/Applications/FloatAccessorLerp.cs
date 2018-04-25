// Author(s): Paul Calande
// Lerps an accessor value based on a timer.

using UnityEngine;

public class FloatAccessorLerp : SingleAccessorLerp
    <float, FloatAccessor>
{
    protected override float Lerp(float start, float end, float normalizedTime)
    {
        return Mathf.Lerp(start, end, normalizedTime);
    }
}