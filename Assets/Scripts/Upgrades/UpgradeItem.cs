// Author(s): Paul Calande
// Upgrade item.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    public delegate void CollectedHandler();
    public event CollectedHandler Collected;

    [SerializeField]
    [Tooltip("The item's sprite renderer.")]
    SpriteRenderer render;
    [SerializeField]
    [Tooltip("Possible sprites.")]
    SOKVStringToSprite sprites;

    public void SetSprite(string spriteName)
    {
        Sprite sprite;
        if (sprites.TryGetValue(spriteName, out sprite))
        {
            render.sprite = sprite;
        }
    }

    private void OnCollected()
    {
        if (Collected != null)
        {
            Collected();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tongue"))
        {
            OnCollected();
        }
    }
}