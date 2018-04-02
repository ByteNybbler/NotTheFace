// Author(s): Paul Calande
// A wrapper class for an event which can have a name.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedEvent
{
    // The actual event itself.
    public delegate void InvokedHandler();
    private event InvokedHandler Invoked;

    // The name used to identify this event.
    string name;

    public NamedEvent(string name)
    {
        this.name = name;
    }

    // Returns the name of the named event.
    public string GetName()
    {
        return name;
    }

    // Adds a new handler to the event.
    public void AddCallback(InvokedHandler callback)
    {
        Invoked += callback;
    }

    // Invokes the event.
    public void OnInvoked()
    {
        if (Invoked != null)
        {
            Invoked();
        }
    }
}