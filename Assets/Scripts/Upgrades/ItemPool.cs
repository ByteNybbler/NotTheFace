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
        itemReader.AddCallbackInt("laser damage bonus", player.AddLaserDamage);
        itemReader.AddCallbackInt("contact damage per second bonus", player.AddContactDamagePerSecond);
        itemReader.AddCallbackInt("damaged explosion damage bonus", player.AddEggExplosionDamage);
        itemReader.AddCallbackInt("headbutt explosion damage bonus", player.AddHeadbuttExplosionDamage);
        itemReader.AddCallbackFloat("jump velocity bonus", player.AddJumpVelocity);
        itemReader.AddCallbackFloat("horizontal acceleration bonus", player.AddHorizontalAcceleration);
        itemReader.AddCallbackFloat("max horizontal speed bonus", player.AddMaxHorizontalSpeed);

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
        string identifier = item.GetName();
        itemText.Appear(identifier);
        player.AddItemVisualEffect(identifier);
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