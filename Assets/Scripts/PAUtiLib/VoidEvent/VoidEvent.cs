// Author(s): Paul Calande
// MonoBehaviour wrapper for a zero-argument event.
// Useful for stringing together logic from different components.
// To be inherited by classes that fire these zero-argument events.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEvent : MonoBehaviour
{
    // Invoked when the Fire method is called.
    public delegate void FiredHandler();
    event FiredHandler Fired;

    public void Subscribe(FiredHandler Callback)
    {
        Fired += Callback;
    }

    // Call this method in derived classes.
    public void OnFired()
    {
        if (Fired != null)
        {
            Fired();
        }
    }
}