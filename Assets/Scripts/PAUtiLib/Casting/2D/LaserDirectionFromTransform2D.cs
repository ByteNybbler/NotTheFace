// Author(s): Paul Calande
// Script for setting a ScaledLaser's direction based on its transform's rotation and scale.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDirectionFromTransform2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The laser to change the direction of.")]
    Laser2D laser;
    [SerializeField]
    [Tooltip("How many degrees to offset the laser's direction by.")]
    float degreesOffset;

    private float GetDegrees()
    {
        float result = transform.rotation.eulerAngles.z + degreesOffset;
        Vector2 scale = laser.transform.lossyScale;
        if (scale.x < 0.0f)
        {
            result = UtilCircle.MirrorAngleHorizontal(result);
        }
        if (scale.y < 0.0f)
        {
            result = UtilCircle.MirrorAngleVertical(result);
        }
        return result;
    }

    private void UpdateDirection()
    {
        laser.SetDirection(GetDegrees());
    }

    private void Start()
    {
        laser.UpdateScaleStarted += UpdateDirection;
    }
}