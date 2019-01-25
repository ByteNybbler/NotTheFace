// Author(s): Paul Calande
// Lerps an accessor value based on a timer.

using UnityEngine;

public class FloatAccessorLerpFromCurrent : SingleAccessorInterpolateFromCurrent
    <float, FloatAccessor>
{
    protected override void OverrideThis()
    {
        SetInterpolateFunction(Mathf.Lerp);
    }
}