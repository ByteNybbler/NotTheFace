// Author(s): Paul Calande
// A trigger that applies a modifier to an object while the object is inside it.
// To be used as a base class.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TValue is the type of value modified by the trigger.
// TMonoIndexedList is the component interfaced with during the trigger collisions.
public class ModifierRegion2D<TValue, TMonoIndexedList> : MonoBehaviour
    where TMonoIndexedList : MonoIndexedList<TValue>
{
    [SerializeField]
    [Tooltip("Only objects satisfying this tag group will be affected by this region.")]
    SOTagGroup tags;
    [SerializeField]
    [Tooltip("The modifier induced by the trigger.")]
    TValue modifier;

    // Maps each MonoIndexedList reference to the index of the factor applied to it.
    // There is one key-value pair for every GameObject within the trigger.
    Dictionary<TMonoIndexedList, int> indices = new Dictionary<TMonoIndexedList, int>();

    // Changes the modifier induced by the region.
    public void SetModifier(TValue value)
    {
        modifier = value;
        // Update this factor for objects already within the trigger.
        foreach (KeyValuePair<TMonoIndexedList, int> pair in indices)
        {
            TMonoIndexedList modifiers = pair.Key;
            int index = pair.Value;
            modifiers.Set(index, modifier);
        }
    }

    // Returns the modifier induced by the region.
    public TValue GetModifier()
    {
        return modifier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tags.IsValid(collision))
        {
            TMonoIndexedList modifiersComponent = collision.GetComponent<TMonoIndexedList>();
            if (modifiersComponent != null)
            {
                int index = modifiersComponent.Add(modifier);
                indices.Add(modifiersComponent, index);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tags.IsValid(collision))
        {
            TMonoIndexedList modifiersComponent = collision.GetComponent<TMonoIndexedList>();
            if (modifiersComponent != null)
            {
                int index;
                if (indices.TryGetValue(modifiersComponent, out index))
                {
                    modifiersComponent.Remove(index);
                    indices.Remove(modifiersComponent);
                }
            }
        }
    }
}