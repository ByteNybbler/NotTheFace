// Author(s): Paul Calande
// Upgrade item.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    [Tooltip("Text for the item name. For testing purposes.")]
    Text textItemName;

    public void SetSprite(string spriteName)
    {
        Sprite sprite;
        if (sprites.TryGetValue(spriteName, out sprite))
        {
            render.sprite = sprite;
        }
    }

    public void SetItemName(string itemName)
    {
        textItemName.text = itemName;
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