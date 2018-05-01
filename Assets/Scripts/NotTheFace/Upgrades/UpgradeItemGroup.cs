// Author(s): Paul Calande
// A group of upgrade items.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemGroup : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The room that this upgrade item group belongs to.")]
    Room room;
    [SerializeField]
    [Tooltip("The prefab to use for the upgrade items.")]
    GameObject prefabUpgradeItem;
    [SerializeField]
    [Tooltip("An array of spawn points to use for upgrade items.")]
    GameObject[] spawnPoints;

    // The item pool to fetch items from.
    ItemPool itemPool;

    private void Start()
    {
        itemPool = room.GetItemPool();
        if (room.GetStatus() == Room.Status.Active)
        {
            SpawnItems();
        }
        else
        {
            room.RoomStarted += SpawnItems;
        }
    }

    private void SpawnItem(Transform parent, NamedEvent itemPoolItem)
    {
        GameObject obj = Instantiate(prefabUpgradeItem, parent);
        obj.transform.localPosition = Vector3.zero;
        UpgradeItem item = obj.GetComponent<UpgradeItem>();
        item.Collected += itemPoolItem.OnInvoked;
        item.Collected += DestroyItems;
        string itemName = itemPoolItem.GetName();
        item.SetSprite(itemName);
        item.SetItemName(UtilTranslate.ItemName(itemName));
        item.SetItemProperties(itemPoolItem.GetDescriptionList("\n"));
    }

    private void SpawnItems()
    {
        int itemPoolCount = itemPool.GetUnclaimedCount();
        if (itemPoolCount == 0)
        {
            DestroyItems();
            return;
        }
        int spawnCount = spawnPoints.Length;
        if (spawnCount > itemPoolCount)
        {
            spawnCount = itemPoolCount;
        }
        List<NamedEvent> items = itemPool.GetRandomItemsUnique(spawnCount);
        for (int i = 0; i < spawnCount; ++i)
        {
            SpawnItem(spawnPoints[i].transform, items[i]);
        }
    }

    private void DestroyItems()
    {
        foreach (GameObject spawnPoint in spawnPoints)
        {
            Destroy(spawnPoint);
        }
        room.FinishRoom();
    }
}