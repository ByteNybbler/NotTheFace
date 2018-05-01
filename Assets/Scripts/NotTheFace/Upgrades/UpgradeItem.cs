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
    [Tooltip("Accessor to the item's SpriteRenderer.")]
    SpriteAccessor accessor;
    [SerializeField]
    [Tooltip("Possible sprites.")]
    SOKVStringToSprite sprites;
    [SerializeField]
    [Tooltip("Text for the item name.")]
    Text textItemName;
    [SerializeField]
    [Tooltip("Text for the item properties.")]
    Text textItemProperties;
    [SerializeField]
    [Tooltip("Damage component that triggers the item getting picked up.")]
    TriggerEventDamage2D damageEvent;

    private void Start()
    {
        damageEvent.Damaged += delegate { OnCollected(); };
    }

    public void SetSprite(string spriteName)
    {
        Sprite sprite;
        if (sprites.TryGetValue(spriteName, out sprite))
        {
            accessor.Set(sprite);
        }
    }

    public void SetItemName(string itemName)
    {
        textItemName.text = itemName;
    }

    public void SetItemProperties(string itemProperties)
    {
        textItemProperties.text = itemProperties;
    }

    private void OnCollected()
    {
        if (Collected != null)
        {
            Collected();
        }
    }
}