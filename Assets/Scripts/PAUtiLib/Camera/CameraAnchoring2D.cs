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

    // Returns the width of the camera view in world units.
    public float GetWidth()
    {
        return GetHeight() * cam.aspect;
    }

    // Returns the height of the camera view in world units.
    public float GetHeight()
    {
        return 2 * cam.orthographicSize;
    }

    // Adjust the orthographic size to fit the given width, in world units.
    public void FitWidth(float width)
    {
        cam.orthographicSize = width / (2 * cam.aspect);
    }

    // Adjust the orthographic size to fit the given height, in world units.
    public void FitHeight(float height)
    {
        cam.orthographicSize = height * 0.5f;
    }

    public float GetHalfWidth()
    {
        return GetWidth() * 0.5f;
    }

    public float GetHalfHeight()
    {
        return cam.orthographicSize;
    }

    // Positions and resizes the camera so that the camera is placed between
    // two given points and has edges that contain those points.
    public void AnchorBetween(Vector2 first, Vector2 second)
    {
        transform.position = Vector2.Lerp(first, second, 0.5f);
        float differenceX = second.x - first.x;
        float differenceY = second.y - first.y;
        int signX = UtilMath.SignWithZero(differenceX);
        int signY = UtilMath.SignWithZero(differenceY);
        float targetWidth = Mathf.Abs(differenceX);
        float targetHeight = Mathf.Abs(differenceY);
        if (signX == 0)
        {
            FitHeight(targetHeight);
        }
        else if (signY == 0)
        {
            FitWidth(targetWidth);
        }
        else
        {
            if (targetWidth / targetHeight < cam.aspect)
            {
                FitHeight(targetHeight);
            }
            else
            {
                FitWidth(targetWidth);
            }
        }
    }

    // Positions and resizes the camera so that a given edge of the camera matches
    // the line formed by the given vertices. Each vertex corresponds to one corner
    // of the camera. The vertices should be defined in a clockwise order.
    // If the given edge is diagonal, nothing happens.
    // (TODO: Perhaps diagonal edges should rotate the camera to match the edge?)
    public void AnchorClockwiseEdge(Vector2 vertexFirst, Vector2 vertexSecond)
    {
        float differenceX = vertexSecond.x - vertexFirst.x;
        float differenceY = vertexSecond.y - vertexFirst.y;
        int signX = UtilMath.SignWithZero(differenceX);
        int signY = UtilMath.SignWithZero(differenceY);
        if (signX == 0)
        {
            FitHeight(Mathf.Abs(differenceY));
            transform.position = new Vector2(vertexFirst.x + GetHalfWidth() * signY,
                vertexFirst.y + GetHalfHeight() * signY);
        }
        else if (signY == 0)
        {
            FitWidth(Mathf.Abs(differenceX));
            transform.position = new Vector2(vertexFirst.x + GetHalfWidth() * signX,
                vertexFirst.y - GetHalfHeight() * signX);
        }
        else
        {
            // The edge is diagonal.
            Debug.LogWarning("CameraAnchoring2D: Attempted to anchor to diagonal edge.");
        }
    }
}