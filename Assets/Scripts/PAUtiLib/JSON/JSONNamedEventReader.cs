// Author(s): Paul Calande
// Reads data for NamedEvents from JSON nodes.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONNamedEventReader
{
    // The type T is the type of the value of the JSON node being read.
    public delegate void EventCallback<T>(T value);

    // The current node reader being used.
    JSONNodeReader nodeReader;
    // The key name of the JSON nodes containing the NamedEvent names.
    string nameKey;
    // The current NamedEvent being read to.
    NamedEvent currentEvent;

    // The following Dictionaries determine which JSON node names to associate
    // with which callbacks.
    // Node name mapped to callback function.
    Dictionary<string, EventCallback<int>> nameToCallbackInt
        = new Dictionary<string, EventCallback<int>>();

    // Constructor.
    public JSONNamedEventReader(string nameKey)
    {
        this.nameKey = nameKey;
    }

    // The callback takes one integer paremeter.
    public void AddCallbackInt(string nodeName, EventCallback<int> callback)
    {
        nameToCallbackInt[nodeName] = callback;
    }

    // Reads from the given node and returns the resulting NamedEvent.
    public NamedEvent Read(JSONNodeReader nodeReader)
    {
        this.nodeReader = nodeReader;
        string identifier = nodeReader.Get(nameKey, "ERROR");
        currentEvent = new NamedEvent(identifier);

        // Read all callback JSON node data.
        foreach (KeyValuePair<string, EventCallback<int>> pair in nameToCallbackInt)
        {
            TryAddCallback(pair.Key, pair.Value);
        }

        return currentEvent;
    }

    // Tries to add a callback to the NamedEvent based on the given node name.
    // The callback parameter is assigned the value associated with the node.
    private void TryAddCallback<T>(string nodeName, EventCallback<T> callback)
    {
        T value;
        if (nodeReader.TryGet(nodeName, out value))
        {
            currentEvent.AddCallback(() => callback(value));
        }
    }
}