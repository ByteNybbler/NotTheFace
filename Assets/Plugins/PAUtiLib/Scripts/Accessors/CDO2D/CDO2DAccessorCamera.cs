// Author(s): Paul Calande
// CDO2DAccessor support for Camera component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDO2DAccessorCamera : SingleAccessorConnection
    <CameraDataOrtho2D, CDO2DAccessor, Camera>
{
    protected override void Set(CameraDataOrtho2D data)
    {
        data.AssignTo(connected);
    }

    protected override CameraDataOrtho2D Get()
    {
        return new CameraDataOrtho2D(connected);
    }
}