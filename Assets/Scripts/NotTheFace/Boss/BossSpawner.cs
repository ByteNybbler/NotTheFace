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
        BossPool bp = room.GetBossPool();
        GameObject obj = Instantiate(prefabBoss, transform);
        obj.transform.localPosition = Vector3.zero;
        Health health = obj.GetComponent<Health>();
        health.Died += room.FinishRoom;
        health.Died += bp.OnDifficultyIncreased;
        health.Died += room.IncrementBossesKilled;
        bossHealthMeter.SetHealth(health);
        Boss boss = obj.GetComponent<Boss>();
        boss.SetData(bp.GetRandomBoss());
        boss.SetRefs(new Boss.Refs(room));
    }
}