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
    NTFPlayer player;
    [SerializeField]
    [Tooltip("The item text.")]
    ItemText itemText;

    // The items that are still in the pool.
    ClaimableElements<NamedEvent> pool = new ClaimableElements<NamedEvent>();

    private void Awake()
    {
        JSONNodeReader jsonReader = new JSONNodeReader(fileItems);

        JSONNamedEventReader itemReader = new JSONNamedEventReader("identifier");
        itemReader.AddCallbackInt("health bonus", player.AddMaxHealth, "+{0} health");
        itemReader.AddCallbackInt("tongue damage bonus", player.AddTongueDamage, "+{0} tongue damage");
        itemReader.AddCallbackInt("headbutt damage bonus", player.AddHeadbuttDamage, "+{0} headbutt damage");
        itemReader.AddCallbackInt("laser damage bonus", player.AddLaserDamage, "+{0} laser damage");
        itemReader.AddCallbackInt("contact damage per second bonus", player.AddContactDamagePerSecond, "+{0} contact damage");
        itemReader.AddCallbackInt("damaged explosion damage bonus", player.AddEggExplosionDamage, "+{0} egg damage...?");
        itemReader.AddCallbackInt("headbutt explosion damage bonus", player.AddHeadbuttExplosionDamage, "+{0} headbutt explosion damage");
        itemReader.AddCallbackFloat("jump velocity bonus", player.AddJumpVelocity, "+{0} jump height");
        itemReader.AddCallbackFloat("horizontal acceleration bonus", player.AddHorizontalAcceleration, "+{0} acceleration");
        itemReader.AddCallbackFloat("max horizontal speed bonus", player.AddMaxHorizontalSpeed, "+{0} max speed");
        itemReader.AddCallbackBool("press down for shield", player.SetHasSoap, "Press down for shield");

        JSONArrayReader itemsReader = jsonReader.Get<JSONArrayReader>("items");
        JSONNodeReader itemNodeReader;
        while (itemsReader.GetNextNode(out itemNodeReader))
        {
            bool blacklist = itemNodeReader.Get("BLACKLIST", false);
            if (!blacklist)
            {
                NamedEvent item = itemReader.Read(itemNodeReader);
                item.AddCallback(() => Claim(item));
                pool.AddUnclaimed(item);
            }
        }
    }

    // Callback for when the player collects the item.
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
    // The index of each returned item will be different.
    public List<NamedEvent> GetRandomItemsUnique(int count)
    {
        return pool.GetRandomElementsUniqueUnclaimed(count);
    }
}