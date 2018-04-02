// Author(s): Paul Calande
// Base class for a component that has indexed factors.
// This is useful for making an object susceptible to many different factor modifications.
// The actual IndexedFactors object should have its callback set from the derived class.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HasIndexedFactors : MonoBehaviour, IIndexedList<float>
{
    // Invoked when the product is updated.
    event IndexedFactors.ProductUpdatedHandler ProductUpdated;

    // The collection of factors.
    IndexedFactors factors;

    private void Awake()
    {
        factors = new IndexedFactors(OnProductUpdated);
    }

    private void OnProductUpdated(float product)
    {
        if (ProductUpdated != null)
        {
            ProductUpdated(product);
        }
    }

    // Call this in the derived class to add a callback.
    public void AddProductUpdatedCallback(IndexedFactors.ProductUpdatedHandler productUpdated)
    {
        ProductUpdated += productUpdated;
    }

    public int Add(float factor)
    {
        return factors.Add(factor);
    }

    public void Remove(int index)
    {
        factors.Remove(index);
    }

    public void Set(int index, float factor)
    {
        factors.Set(index, factor);
    }
}