// Author(s): Paul Calande
// Makes an object move towards a position in world space at a constant rate.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToPosition2D : MonoBehaviour
{
    // Invoked when the object reaches its destination.
    public delegate void CompletedHandler();
    public event CompletedHandler Completed;

    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    Mover2D mover;

    // The velocity at which the object is moving.
    private Vector2 velocity;
    // The destination that the object is heading towards.
    private Vector2 destination;
    // Whether the object is currently moving towards a destination.
    bool lerping = false;

    // When this method is called, the GameObject moves towards the destination 
    // at the given speed and will keep moving until it reaches the destination.
    public void LerpToAtSpeed(Vector2 destination, float speed)
    {
        this.destination = destination;
        velocity = (destination - mover.GetPosition()).normalized * speed;
        lerping = true;
    }

    // When this method is called, the GameObject moves towards the destination
    // with a velocity to reach the destination in the given amount of seconds.
    public void LerpToInTime(Vector2 destination, float seconds)
    {
        this.destination = destination;
        velocity = UtilPredict.ConstantVelocity(mover.GetPosition(), destination, seconds);
        lerping = true;
    }

    private void FixedUpdate()
    {
        if (lerping)
        {
            Vector2 stepVelocity = velocity * timeScale.DeltaTime();
            if (stepVelocity.sqrMagnitude >
                (destination - mover.GetPosition()).sqrMagnitude)
            {
                mover.TeleportPosition(destination);
                OnCompleted();
            }
            else
            {
                mover.OffsetPosition(stepVelocity);
            }
        }
    }

    // Invoke the Completed event.
    void OnCompleted()
    {
        lerping = false;
        if (Completed != null)
        {
            Completed();
        }
    }
}