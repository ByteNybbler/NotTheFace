// Author(s): Paul Calande
// MonoBehaviour wrapper for an IndexedList.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoIndexedList<TValue> : MonoBehaviour, IIndexedList<TValue>
{
    // Invoked when the list is modified.
    event IndexedList<TValue>.ModifiedHandler Modified;

    // The encapsulated collection.
    IndexedList<TValue> list;

    private void Awake()
    {
        list = new IndexedList<TValue>(OnModified);
    }

    public void SubscribeToModified(IndexedList<TValue>.ModifiedHandler Callback)
    {
        Modified += Callback;
    }

    public int Add(TValue value)
    {
        return list.Add(value);
    }

    public void Remove(int index)
    {
        list.Remove(index);
    }

    public void Set(int index, TValue value)
    {
        list.Set(index, value);
    }

    private void OnModified(TValue[] elements)
    {
        if (Modified != null)
        {
            Modified(elements);
        }
    }
}