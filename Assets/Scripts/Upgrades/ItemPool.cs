// Author(s): Paul Calande
// Item pool.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPool : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The items JSON.")]
    TextAsset fileItems;
    [SerializeField]
    [Tooltip("Reference to the player.")]
    Player player;
    [SerializeField]
    [Tooltip("The item name text.")]
    Text textItemName;

    // The timer that determines when the item-related text will exit the screen.
    Timer timerItemText;
    // The translator for translating the item text.
    Translator translator;

    // The items that are still in the pool.
    ClaimableElements<NamedEvent> pool = new ClaimableElements<NamedEvent>();

    private void Awake()
    {
        ItemTextVanish();

        JSONNodeReader jsonReader = new JSONNodeReader(fileItems);

        float timerItemSeconds = jsonReader.Get("seconds to display item text", 3.0f);
        timerItemText = new Timer(timerItemSeconds, ItemTextVanish, false, false);

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
            item.AddCallback(() => ItemTextAppear(item.GetName()));

            pool.AddUnclaimed(item);
        }
    }

    private void Claim(NamedEvent item)
    {
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

    private void Start()
    {
        translator = ServiceLocator.GetTranslator();
    }

    private void ItemTextVanish(float secondsOverflow = 0.0f)
    {
        textItemName.gameObject.SetActive(false);
    }

    private void ItemTextAppear(string identifier)
    {
        textItemName.gameObject.SetActive(true);
        timerItemText.Reset();
        timerItemText.Start();

        textItemName.text = translator.Translate("Item", identifier, "Name");
    }

    private void FixedUpdate()
    {
        timerItemText.Tick(timeScale.DeltaTime());
    }
}