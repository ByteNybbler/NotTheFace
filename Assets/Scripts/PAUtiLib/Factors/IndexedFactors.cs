// Author(s): Paul Calande
// A collection of factors that get multiplied together to create a single product.
// This is useful for making an object susceptible to many different modifications
// of a single attribute while preserving each individual factor.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexedFactors : IIndexedList<float>
{
    // The callback invoked when the product changes.
    public delegate void ProductUpdatedHandler(float product);
    ProductUpdatedHandler productUpdated;

    IndexedList<float> factors = new IndexedList<float>();

    public IndexedFactors(ProductUpdatedHandler productUpdated)
    {
        this.productUpdated = productUpdated;
    }

    public void SetProductUpdatedCallback(ProductUpdatedHandler productUpdated)
    {
        this.productUpdated = productUpdated;
        UpdateProduct();
    }

    // Adjusts the product based on the collection of factors.
    private void UpdateProduct()
    {
        float product = 1.0f;
        foreach (float fact in factors.GetAllValues())
        {
            product *= fact;
        }
        if (productUpdated != null)
        {
            productUpdated(product);
        }
    }

    // Adds a factor to the collection and adjusts the product accordingly.
    // Returns the index of the factor.
    public int Add(float factor)
    {
        int index = factors.Add(factor);
        UpdateProduct();
        return index;
    }

    // Removes the factor with the given index.
    // Adjusts the product accordingly.
    public void Remove(int index)
    {
        factors.Remove(index);
        UpdateProduct();
    }

    // Sets the factor with the given index to the given value.
    // Adjusts the product accordingly.
    public void Set(int index, float factor)
    {
        factors.Set(index, factor);
        UpdateProduct();
    }
}