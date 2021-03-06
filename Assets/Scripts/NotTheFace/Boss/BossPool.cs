﻿// Author(s): Paul Calande
// The pool of bosses used in Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPool : MonoBehaviour
{
    public delegate void DifficultyIncreasedHandler();
    event DifficultyIncreasedHandler DifficultyIncreased;

    [SerializeField]
    [Tooltip("Boss data.")]
    TextAsset fileBosses;
    [SerializeField]
    [Tooltip("Spike warning prefabs.")]
    SOKVStringToGameObject spikeWarnings;
    [SerializeField]
    SOKVStringToRuntimeAnimatorController spikeAnimators;
    [SerializeField]
    SOKVStringToRuntimeAnimatorController orbAnimators;

    // The pool of bosses.
    List<Boss.Data> pool = new List<Boss.Data>();

    // Read data for a floor spike.
    private FloorSpike.Data ReadFloorSpike(JSONNodeReader attackNodeReader, string appearance)
    {
        GameObject spikeWarning;
        spikeWarnings.TryGetValue(appearance, out spikeWarning);

        FloorSpike.Data d = new FloorSpike.Data(
            attackNodeReader.Get("damage", 20),
            spikeWarning,
            attackNodeReader.Get("seconds of warning", 1.0f),
            attackNodeReader.Get("seconds of rising", 0.2f),
            attackNodeReader.Get("seconds to idle", 3.0f),
            attackNodeReader.Get("seconds to lower", 0.5f),
            attackNodeReader.Get("height to rise", 1.0f),
            attackNodeReader.Get("height to rise variance", 0.0f));

        int damageBonusPerArena = attackNodeReader.Get("damage bonus per arena", 0);
        DifficultyIncreased += () => d.damage += damageBonusPerArena;

        return d;
    }

    private void Awake()
    {
        JSONNodeReader jsonReader = new JSONNodeReader(fileBosses);
        JSONArrayReader bossesReader = jsonReader.Get<JSONArrayReader>("bosses");
        JSONNodeReader bossNodeReader;
        while (bossesReader.GetNextNode(out bossNodeReader))
        {
            List<Boss.AttackGroup> attackGroups = new List<Boss.AttackGroup>();

            JSONArrayReader attackGroupsReader =
                bossNodeReader.Get<JSONArrayReader>("attack groups");
            JSONNodeReader attackGroupNodeReader;
            while (attackGroupsReader.GetNextNode(out attackGroupNodeReader))
            {
                Boss.AttackGroup attackGroup = new Boss.AttackGroup(
                    attackGroupNodeReader.Get("seconds of cooldown", 2.0f),
                    attackGroupNodeReader.Get("seconds of cooldown variance", 0.5f));

                float secondsOfCooldownBonusPerArena =
                    attackGroupNodeReader.Get("seconds of cooldown bonus per arena", 0.0f);
                float secondsOfCooldownMin = attackGroupNodeReader.Get("seconds of cooldown min", 0.1f);

                DifficultyIncreased += () => attackGroup.SetSecondsOfCooldown(
                    UtilApproach.Float(attackGroup.GetSecondsOfCooldown(),
                    secondsOfCooldownMin,
                    -secondsOfCooldownBonusPerArena)
                    );

                JSONArrayReader attacksReader = attackGroupNodeReader.Get<JSONArrayReader>("attacks");
                JSONNodeReader attackNodeReader;
                while (attacksReader.GetNextNode(out attackNodeReader))
                {
                    string identifier = attackNodeReader.Get("identifier", "ERROR");
                    string appearance = attackNodeReader.Get("appearance", "ERROR");
                    if (identifier == "FloorSpikes")
                    {
                        float count = attackNodeReader.Get("count", 3);

                        RuntimeAnimatorController rac;
                        spikeAnimators.TryGetValue(appearance, out rac);

                        FloorSpike.Data d = ReadFloorSpike(attackNodeReader, appearance);

                        attackGroup.AddAttack(x => Boss.FloorSpikes(x,
                            attackGroup, d, rac, count));

                        float countBonusPerArena = attackNodeReader.Get("count bonus per arena", 0.0f);
                        float countMax = attackNodeReader.Get("count max", 6);
                        DifficultyIncreased +=
                            () => count = UtilApproach.Float(count, countMax, countBonusPerArena);
                    }
                    else if (identifier == "FloorSpikeTargetPlayer")
                    {
                        RuntimeAnimatorController rac;
                        spikeAnimators.TryGetValue(appearance, out rac);

                        FloorSpike.Data d = ReadFloorSpike(attackNodeReader, appearance);

                        attackGroup.AddAttack(x => Boss.FloorSpikeTargetPlayer(x,
                            attackGroup, d, rac));
                    }
                    else if (identifier == "Orb")
                    {
                        RuntimeAnimatorController rac;
                        orbAnimators.TryGetValue(appearance, out rac);

                        BossOrb.Data d = new BossOrb.Data(
                            attackNodeReader.Get("damage", 20),
                            attackNodeReader.Get("seconds to idle", 3.0f),
                            attackNodeReader.Get("seconds to idle variance", 2.0f),
                            attackNodeReader.Get("seconds to move to center", 0.2f),
                            attackNodeReader.Get("seconds to move to bottom", 0.8f),
                            attackNodeReader.Get("horizontal speed", 8.0f));

                        string positionName = attackNodeReader.Get("position", "ERROR");

                        attackGroup.AddAttack(x => Boss.SpawnOrb(x, attackGroup,
                            d, positionName, rac));

                        int damageBonusPerArena = attackNodeReader.Get("damage bonus per arena", 0);
                        int damageMax = attackNodeReader.Get("damage max", 10000);
                        DifficultyIncreased += () => d.damage =
                            UtilApproach.Int(d.damage, damageMax, damageBonusPerArena);
                    }
                    /*
                    else if (identifier == "HorizontalProjectile")
                    {
                        int damage = attackNodeReader.Get("damage", 20);
                        float speed = attackNodeReader.Get("speed", 8.0f);
                        float spawnHeight = attackNodeReader.Get("spawn height", 1.0f);
                        float seconds = attackNodeReader.Get("seconds to wait after attack",
                            1.0f);
                        Velocity2D.Data d = new Velocity2D.Data(new Vector2(-speed, 0.0f));
                        attacks.Add(x => Boss.FireProjectile(x, d, damage, spawnHeight,
                            seconds));
                    }
                    */
                }

                attackGroups.Add(attackGroup);
            }

            Boss.Data boss = new Boss.Data(
                bossNodeReader.Get("identifier", "ERROR"),
                bossNodeReader.Get("base health", 1),
                attackGroups);

            int healthBonusPerArena = bossNodeReader.Get("health bonus per arena", 0);
            DifficultyIncreased += () => boss.baseHealth += healthBonusPerArena;

            pool.Add(boss);
        }
    }

    public Boss.Data GetRandomBoss()
    {
        return UtilRandom.GetRandomElement(pool);
    }

    // Increase the difficulty by one boss room.
    public void OnDifficultyIncreased()
    {
        if (DifficultyIncreased != null)
        {
            DifficultyIncreased();
        }
    }
}