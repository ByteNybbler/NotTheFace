// Author(s): Paul Calande
// Wrapper class for a JSON node, with some additional convenient methods.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class JSONNodeReader
{
    // The node to read from.
    JSONNode myNode;

    // Construct based on a file reference, making its root the node to be read from.
    public JSONNodeReader(TextAsset file)
    {
        SetFile(file);
    }

    // Construct based on a given node.
    public JSONNodeReader(JSONNode node)
    {
        myNode = node;
    }

    // Change the file being read.
    public void SetFile(TextAsset file)
    {
        myNode = JSON.Parse(file.ToString());
    }

    // For template specialization.
    interface IAsType<T>
    {
        T As(JSONNode node);
    }
    class AsType<T> : IAsType<T>
    {
        public static readonly IAsType<T> instance
            = AsType.instance as IAsType<T> ?? new AsType<T>();

        T IAsType<T>.As(JSONNode node)
        {
            throw new System.NotSupportedException();
        }
    }
    class AsType : IAsType<int>, IAsType<bool>, IAsType<float>, IAsType<double>,
        IAsType<string>, IAsType<JSONNodeReader>, IAsType<JSONArrayReader>
    {
        public static AsType instance = new AsType();

        int IAsType<int>.As(JSONNode node)
        {
            return node.AsInt;
        }
        bool IAsType<bool>.As(JSONNode node)
        {
            return node.AsBool;
        }
        float IAsType<float>.As(JSONNode node)
        {
            return node.AsFloat;
        }
        double IAsType<double>.As(JSONNode node)
        {
            return node.AsDouble;
        }
        string IAsType<string>.As(JSONNode node)
        {
            return node;
        }
        JSONNodeReader IAsType<JSONNodeReader>.As(JSONNode node)
        {
            return new JSONNodeReader(node);
        }
        JSONArrayReader IAsType<JSONArrayReader>.As(JSONNode node)
        {
            return new JSONArrayReader(node.AsArray);
        }
    }
    private static T As<T>(JSONNode node)
    {
        return AsType<T>.instance.As(node);
    }

    // Get will return the value stored in the node.
    // If the node does not exist, the default value will be returned.
    // This means the method will return a value no matter what,
    // assuming a supported type is used.
    public T Get<T>(string nodeName, T defaultValue)
    {
        JSONNode node = myNode[nodeName];
        if (node == null)
        {
            return defaultValue;
        }
        else
        {
            return As<T>(node);
        }
    }

    // Overload for types that support null.
    // This will return null as the default value.
    public T Get<T>(string nodeName) where T : class
    {
        return Get<T>(nodeName, null);
    }

    // TryGet will return a boolean: true if the node exists, or false if it does not.
    // If a value is found, it is returned via the out parameter.
    // If a value is not found, the default-constructed value is returned.
    // TryGet should be used to support alternative behavior if a node does not exist.
    public bool TryGet<T>(string nodeName, out T value)
    {
        JSONNode node = myNode[nodeName];
        if (node == null)
        {
            value = default(T);
            return false;
        }
        else
        {
            value = As<T>(node);
            return true;
        }
    }
}