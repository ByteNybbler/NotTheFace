// Author(s): Paul Calande
// Destroys certain GameObjects that exit a trigger region.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingRegion2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Collection of tags to bound within the trigger region.")]
    string[] boundedTags;

    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (string tagName in boundedTags)
        {
            if (other.CompareTag(tagName))
            {
                Destroy(other.transform.root.gameObject);
            }
        }
    }
}