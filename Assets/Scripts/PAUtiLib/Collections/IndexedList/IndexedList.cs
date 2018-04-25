// Author(s): Paul Calande
// A Dictionary where every integer key is unique and corresponds to some value.
// These integer keys can be used to access their corresponding values.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IndexedList<T> : IIndexedList<T>
{
    // Invoked when an element is added, removed, or changed.
    // The parameter is every element in the list.
    public delegate void ModifiedHandler(T[] elements);
    ModifiedHandler Modified;

    // The "list" of elements.
    Dictionary<int, T> list = new Dictionary<int, T>();

    // Constructor.
    public IndexedList(ModifiedHandler Callback = null)
    {
        Subscribe(Callback);
    }

    // Subscribe to Modified.
    public void Subscribe(ModifiedHandler Callback)
    {
        Modified = Callback;
        OnModified();
    }

    // Returns the first free key in the collection, starting at 0 and counting up.
    private int GetFirstFreeKey()
    {
        int key = 0;
        while (list.ContainsKey(key))
        {
            ++key;
        }
        return key;
    }

    // Adds a value to the list and returns its key.
    public int Add(T value)
    {
        int key = GetFirstFreeKey();
        list.Add(key, value);
        OnModified();
        return key;
    }

    // Removes the value corresponding to the given key.
    public void Remove(int key)
    {
        list.Remove(key);
        OnModified();
    }

    // Sets the value corresponding to the given key.
    public void Set(int key, T value)
    {
        list[key] = value;
        OnModified();
    }

    // Attempts to retrieve the value corresponding to the given key.
    // Returns true if successful. Returns false otherwise.
    public bool TryGetValue(int key, out T value)
    {
        return list.TryGetValue(key, out value);
    }

    // Returns all values in the collection.
    public T[] GetAllValues()
    {
        return list.Values.ToArray();
    }

    private void OnModified()
    {
        if (Modified != null)
        {
            Modified(GetAllValues());
        }
    }
}