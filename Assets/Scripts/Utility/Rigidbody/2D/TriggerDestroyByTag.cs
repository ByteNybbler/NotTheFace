// Author(s): Paul Calande
// Component for a trigger that makes the GameObject get destroyed upon contact
// with colliders with the given tags.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDestroyByTag : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Collection of tags that will destroy this GameObject.")]
    string[] tags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tagName in tags)
        {
            if (collision.CompareTag(tagName))
            {
                Destroy(gameObject);
            }
        }
    }
}