// Author(s): Paul Calande
// A trigger that applies a time factor to an object while the object is inside it.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRegion : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The time factor induced by the time region.")]
    float timeFactor = 0.5f;

    // Maps each TimeFactors reference to the time factor index applied to it.
    Dictionary<TimeFactors, int> indices = new Dictionary<TimeFactors, int>();

    public void SetTimeFactor(float timeFactor)
    {
        this.timeFactor = timeFactor;
        // Update all of the time factors for objects already within the trigger.
        foreach (KeyValuePair<TimeFactors, int> pair in indices)
        {
            TimeFactors tf = pair.Key;
            int index = pair.Value;
            tf.Set(index, timeFactor);
        }
    }

    public float GetTimeFactor()
    {
        return timeFactor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TimeFactors tf = collision.GetComponent<TimeFactors>();
        if (tf != null)
        {
            int index = tf.Add(timeFactor);
            indices.Add(tf, index);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TimeFactors tf = collision.GetComponent<TimeFactors>();
        if (tf != null)
        {
            int index;
            if (indices.TryGetValue(tf, out index))
            {
                tf.Remove(index);
                indices.Remove(tf);
            }
        }
    }
}