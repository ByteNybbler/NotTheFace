// Author(s): Paul Calande
// Float-based MonoIndexedList that keeps track of the product of its elements.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatProductIndexedList : MonoIndexedList<float>
{
    // Invoked every time the product is updated.
    public delegate void ProductUpdatedHandler(float product);
    event ProductUpdatedHandler ProductUpdated;

    // The current product of the elements of the list.
    float product = 1.0f;

    private void Awake()
    {
        SubscribeToModified(ListModified);
    }

    public void SubscribeToProductUpdated(ProductUpdatedHandler Callback)
    {
        ProductUpdated += Callback;
        Callback(product);
    }

    private void ListModified(float[] elements)
    {
        product = UtilMath.Product(elements);
        OnProductUpdated(product);
    }

    private void OnProductUpdated(float product)
    {
        if (ProductUpdated != null)
        {
            ProductUpdated(product);
        }
    }
}