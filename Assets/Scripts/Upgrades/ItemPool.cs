// Author(s): Paul Calande
// Item pool script for Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The items JSON.")]
    TextAsset fileItems;
    [SerializeField]
    [Tooltip("Reference to the player.")]
    Player player;
    [SerializeField]
    [Tooltip("The item text.")]
    ItemText itemText;

    // The items that are still in the pool.
    ClaimableElements<NamedEvent> pool = new ClaimableElements<NamedEvent>();

    private void Awake()
    {
        JSONNodeReader jsonReader = new JSONNodeReader(fileItems);

        JSONNamedEventReader itemReader = new JSONNamedEventReader("identifier");
        itemReader.AddCallbackInt("health bonus", player.AddMaxHealth);
        itemReader.AddCallbackInt("tongue damage bonus", player.AddTongueDamage);
        itemReader.AddCallbackInt("headbutt damage bonus", player.AddHeadbuttDamage);

        JSONArrayReader itemsReader = jsonReader.Get<JSONArrayReader>("items");
        JSONNodeReader itemNodeReader;
        while (itemsReader.GetNextNode(out itemNodeReader))
        {
            NamedEvent item = itemReader.Read(itemNodeReader);
            item.AddCallback(() => Claim(item));
            pool.AddUnclaimed(item);
        }
    }

    private void Claim(NamedEvent item)
    {
        itemText.Appear(item.GetName());
        pool.Claim(item);
    }

    public int GetUnclaimedCount()
    {
        return pool.GetUnclaimedCount();
    }

    // Returns a collection of items from the pool without removing them.
    // The index of each item returned will be different.
    public List<NamedEvent> GetRandomItemsUnique(int count)
    {
        return pool.GetRandomElementsUniqueUnclaimed(count);
    }
}