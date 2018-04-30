// Author(s): Paul Calande
// Resizes a camera using anchors that can be defined as corners of the camera view.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchoring2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The camera to anchor.")]
    Camera cam;

    // Data about the camera.
    CameraDataOrtho2D cd;

    private void FixedUpdate()
    {
        //
    }
}