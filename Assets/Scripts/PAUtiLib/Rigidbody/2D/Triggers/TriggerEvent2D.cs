// Author(s): Paul Calande
// Wrapper class for an event invoked by a trigger.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent2D : MonoBehaviour
{
    public delegate void InvokedHandler(Collider2D collision);
    private event InvokedHandler Invoked;

    public void Subscribe(InvokedHandler handler)
    {
        Invoked += handler;
    }

    public void Unsubscribe(InvokedHandler handler)
    {
        Invoked -= handler;
    }

    // Invoke the event.
    public void OnInvoke(Collider2D collision)
    {
        if (Invoked != null)
        {
            Invoked(collision);
        }
    }
}