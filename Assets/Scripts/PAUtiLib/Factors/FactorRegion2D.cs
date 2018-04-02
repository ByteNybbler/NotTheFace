// Author(s): Paul Calande
// A trigger that applies a factor to an object while the object is inside it.
// To be used as a base class.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ideally, the generic type parameter should derive from HasIndexedFactors.
// The generic type parameter is the component interfaced with during the trigger collisions.
public class FactorRegion2D<T> : MonoBehaviour where T : MonoBehaviour, IIndexedList<float>
{
    [SerializeField]
    [Tooltip("The factor induced by the region.")]
    float factor = 0.5f;

    // Maps each IndexedFactors reference to the index of the factor applied to it.
    // There is one key value pair for every GameObject within the trigger.
    Dictionary<T, int> indices = new Dictionary<T, int>();

    // Modifies the factor induced by the region.
    public void SetFactor(float factor)
    {
        this.factor = factor;
        // Update this factor for objects already within the trigger.
        foreach (KeyValuePair<T, int> pair in indices)
        {
            T factors = pair.Key;
            int index = pair.Value;
            factors.Set(index, factor);
        }
    }

    // Returns the factor induced by the region.
    public float GetFactor()
    {
        return factor;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        T factors = collision.GetComponent<T>();
        if (factors != null)
        {
            int index = factors.Add(factor);
            indices.Add(factors, index);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        T factors = collision.GetComponent<T>();
        if (factors != null)
        {
            int index;
            if (indices.TryGetValue(factors, out index))
            {
                factors.Remove(index);
                indices.Remove(factors);
            }
        }
    }
}