// Author(s): Paul Calande
// Upgrade item script for Not the Face.

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
    [SerializeField]
    [Tooltip("DamageFromPlayer component that triggers the item getting picked up.")]
    DamageFromPlayer dfp;

    private void Start()
    {
        dfp.Damaged += (int damage) => OnCollected();
    }

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
}