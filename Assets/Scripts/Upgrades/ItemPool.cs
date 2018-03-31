// Author(s): Paul Calande
// Item pool.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPool : MonoBehaviour
{
    // Class representing an item.
    public class Item
    {
        // Invoked when the item is collected.
        public delegate void CollectedHandler();
        public event CollectedHandler Collected;

        // The sprite of the item.
        public Sprite sprite;
        // The identifier string of the item.
        public string identifier;

        public void OnCollected()
        {
            if (Collected != null)
            {
                Collected();
            }
        }
    }

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
    List<Item> pool = new List<Item>();

    private void Awake()
    {
        ItemTextVanish();

        JSONNodeReader jsonReader = new JSONNodeReader(fileItems);

        float timerItemSeconds = jsonReader.Get("seconds to display item text", 3.0f);
        timerItemText = new Timer(timerItemSeconds, ItemTextVanish, false, false);

        JSONArrayReader itemsReader = jsonReader.Get<JSONArrayReader>("items");
        JSONNodeReader itemReader;
        while (itemsReader.GetNextNode(out itemReader))
        {
            Item item = new Item();
            item.identifier = itemReader.Get("identifier", "ERROR");

            item.Collected += () => Collect(item);
            item.Collected += () => ItemTextAppear(item.identifier);

            MakeItemCallback<int>(item, itemReader, "health bonus", player.AddMaxHealth);
            MakeItemCallback<int>(item, itemReader, "tongue damage bonus", player.AddTongueDamage);
            MakeItemCallback<int>(item, itemReader, "headbutt damage bonus", player.AddHeadbuttDamage);

            pool.Add(item);
        }
    }

    // The type T is the type of the value of the JSON node being read.
    private delegate void ItemCallback<T>(T value);
    private void MakeItemCallback<T>(Item item, JSONNodeReader itemReader, string keyName,
        ItemCallback<T> action)
    {
        T value;
        if (itemReader.TryGet(keyName, out value))
        {
            item.Collected += () => action(value);
        }
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

    private void Collect(Item item)
    {
        pool.Remove(item);
    }

    public int Count()
    {
        return pool.Count;
    }

    // Returns a collection of items from the pool without removing them.
    // Each item returned will be different
    public List<Item> FetchRandomUniqueItems(int count)
    {
        List<int> ints = UtilRandom.UniqueIntegersShuffled(count, 0, pool.Count);
        List<Item> result = new List<Item>(count);
        foreach (int i in ints)
        {
            result.Add(pool[i]);
        }
        return result;
    }

    private void FixedUpdate()
    {
        timerItemText.Tick(timeScale.DeltaTime());
    }
}