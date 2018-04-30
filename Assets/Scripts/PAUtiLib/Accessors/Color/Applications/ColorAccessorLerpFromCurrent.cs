// Author(s): Paul Calande
// Lerps an accessor value based on a timer.

using UnityEngine;

public class ColorAccessorLerpFromCurrent : SingleAccessorInterpolateFromCurrent
    <Color, ColorAccessor>
{
    protected override void OverrideThis()
    {
        SetInterpolateFunction(Color.Lerp);
    }
}