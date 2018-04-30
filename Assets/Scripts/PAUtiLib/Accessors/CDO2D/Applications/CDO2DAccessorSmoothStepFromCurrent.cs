// Author(s): Paul Calande
// Interpolates an accessor value based on a timer.

using UnityEngine;

public class CDO2DAccessorSmoothStepFromCurrent : SingleAccessorInterpolateFromCurrent
    <CameraDataOrtho2D, CDO2DAccessor>
{
    protected override void OverrideThis()
    {
        SetInterpolateFunction(CameraDataOrtho2D.SmoothStep);
    }
}