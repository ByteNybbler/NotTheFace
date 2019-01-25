// Author(s): Paul Calande
// A wrapper class for an event which can have a name along with other information.

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
    // Each description corresponds to a single callback of this event.
    List<string> descriptions = new List<string>();

    public NamedEvent(string name)
    {
        this.name = name;
    }

    // Returns the name of the named event.
    public string GetName()
    {
        return name;
    }

    // Adds a new callback to the event, along with a description of the callback.
    public void AddCallback(InvokedHandler Callback, string description = "")
    {
        if (description != "")
        {
            descriptions.Add(description);
        }
        Invoked += Callback;
    }

    // Returns a list of all callback descriptions.
    public string GetDescriptionList(string connector)
    {
        return UtilString.Connect(connector, descriptions.ToArray());
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