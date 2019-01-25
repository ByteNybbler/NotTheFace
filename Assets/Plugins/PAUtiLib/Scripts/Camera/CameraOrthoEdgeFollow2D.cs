// Author(s): Paul Calande
// Orthographic camera follows a Transform when it moves out of the camera's edge.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrthoEdgeFollow2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to read from.")]
    CDO2DAccessor accessor;
    [SerializeField]
    [Tooltip("The interpolator to use to move the camera.")]
    CDO2DAccessorSmoothStepFromCurrent interpolator;
    [SerializeField]
    [Tooltip("The transform to follow.")]
    Transform toFollow;

    private void FixedUpdate()
    {
        if (!interpolator.TimerIsRunning() && toFollow != null)
        {
            CameraDataOrtho2D data = accessor.Get();
            if (toFollow.position.x > data.GetCoordinateRightEdge())
            {
                data.OffsetPosition(Vector2.right * data.GetWidth());
                interpolator.SetTargetValue(data, true);
            }
            else if (toFollow.position.x < data.GetCoordinateLeftEdge())
            {
                data.OffsetPosition(Vector2.left * data.GetWidth());
                interpolator.SetTargetValue(data, true);
            }
        }
    }
}