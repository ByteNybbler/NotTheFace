// Author(s): Paul Calande
// Wrapper class for an event invoked by a trigger.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerEvent2D : MonoBehaviour
{
    // Invoked when the event is triggered.
    public delegate void InvokedHandler(Collider2D collision);
    private event InvokedHandler Invoked;

    [SerializeField]
    [Tooltip("The acceptable tags for the collision.")]
    SOTagGroup tags;

    public void Subscribe(InvokedHandler handler)
    {
        Invoked += handler;
    }

    public void Unsubscribe(InvokedHandler handler)
    {
        Invoked -= handler;
    }

    // Invoke the event.
    protected void OnInvoke(Collider2D collision)
    {
        if (Invoked != null)
        {
            Invoked(collision);
        }
    }

    // Returns true if the given collision can be accepted by the trigger at this time.
    private bool IsValid(Collider2D collision)
    {
        return tags.IsValid(collision) && isActiveAndEnabled;
    }

    protected void TryInvokeEvent(Collider2D collision)
    {
        if (IsValid(collision))
        {
            OnInvoke(collision);
        }
    }
}