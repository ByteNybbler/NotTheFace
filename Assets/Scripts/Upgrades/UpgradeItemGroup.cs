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

    private void Start()
    {
        if (room.GetStatus() == Room.Status.Active)
        {
            SpawnItems();
        }
        else
        {
            room.RoomStarted += SpawnItems;
        }
    }

    private void SpawnItems()
    {
        foreach (GameObject spawnPoint in spawnPoints)
        {
            GameObject obj = Instantiate(prefabUpgradeItem, spawnPoint.transform);
            obj.transform.localPosition = Vector3.zero;
            UpgradeItem item = obj.GetComponent<UpgradeItem>();
            item.Collected += DestroyItems;
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