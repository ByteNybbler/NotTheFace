// Author(s): Paul Calande
// Scales this GameObject along its x axis until it hits an object via raycasting.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser2D : MonoBehaviour
{
    // Invoked when the laser prepares to update its scale.
    public delegate void UpdateScaleStartedHandler();
    event UpdateScaleStartedHandler UpdateScaleStarted;

    [SerializeField]
    [Tooltip("The SpriteRenderer to read the size of in order to scale properly.")]
    SpriteRenderer render;
    [SerializeField]
    [Tooltip("The mask to use for the raycast.")]
    LayerMask layerMask;
    [SerializeField]
    [Tooltip("The maximum distance the laser can go.")]
    float maxDistance = 10000.0f;

    Vector2 direction = Vector2.right;

    public void Subscribe(UpdateScaleStartedHandler Callback)
    {
        UpdateScaleStarted += Callback;
        Callback();
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
        //Debug.Log("Laser2D SetDirection: " + direction);
    }

    public void SetDirection(float degrees)
    {
        SetDirection(Angle.FromDegrees(degrees).GetHeadingVector());
        //SetDirection(UtilHeading2D.HeadingVectorFromDegrees(degrees));
    }

    public void SetMaxDistance(float maxDistance)
    {
        this.maxDistance = maxDistance;
    }

    private void UpdateScale()
    {
        OnUpdateScaleStarted();

        RaycastHit2D hit = Physics2D.Raycast(transform.position,
            direction, maxDistance, layerMask);
        float distanceCovered;
        if (hit)
        {
            distanceCovered = hit.distance;
        }
        else
        {
            distanceCovered = maxDistance;
        }
        float spriteXSize = render.bounds.size.x / transform.localScale.x;
        /*
        Debug.Log("distanceCovered / direction: " + distanceCovered + " / " + direction);
        Debug.Log("render.bounds.size.x: " + render.bounds.size.x);
        Debug.Log("spriteXSize: " + spriteXSize);
        */
        Vector3 scale = transform.localScale;
        scale.x = distanceCovered / spriteXSize;
        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        UpdateScale();
    }

    private void OnEnable()
    {
        UpdateScale();
    }

    private void OnUpdateScaleStarted()
    {
        if (UpdateScaleStarted != null)
        {
            UpdateScaleStarted();
        }
    }
}