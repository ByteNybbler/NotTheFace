// Author(s): Paul Calande
// Anchors a camera between two transforms.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartToCameraAnchorBetweenTransforms : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor for the orthographic camera data.")]
    CDO2DAccessor accessor;
    [SerializeField]
    [Tooltip("The first transform to anchor between.")]
    Transform transformFirst;
    [SerializeField]
    [Tooltip("The second transform to anchor between.")]
    Transform transformSecond;

    private void Start()
    {
        Fire();
    }

    public void Fire()
    {
        CameraDataOrtho2D data = accessor.Get();
        data.AnchorBetween(transformFirst.position, transformSecond.position);
        accessor.Set(data);
    }
}