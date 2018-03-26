// Author(s): Paul Calande
// A component of a Room that spawns a Boss.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The room that this boss spawner inhabits.")]
    Room room;
    [SerializeField]
    [Tooltip("The prefab to use for the boss.")]
    GameObject prefabBoss;
    [SerializeField]
    [Tooltip("The health meter to use for the boss.")]
    HealthToMeter bossHealthMeter;

    private void Start()
    {
        room.RoomStarted += SpawnBoss;
    }

    private void SpawnBoss()
    {
        GameObject obj = Instantiate(prefabBoss, transform);
        obj.transform.localPosition = Vector3.zero;
        Boss boss = obj.GetComponent<Boss>();
        boss.Died += room.FinishRoom;
        bossHealthMeter.SetHealth(obj.GetComponent<Health>());
    }
}
