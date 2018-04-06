// Author(s): Paul Calande
// The pool of bosses used in Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPool : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Boss data.")]
    TextAsset fileBosses;
    [SerializeField]
    [Tooltip("Spike warning prefabs.")]
    SOKVStringToGameObject spikeWarnings;
    [SerializeField]
    SOKVStringToRuntimeAnimatorController spikeAnimators;

    // The pool of bosses.
    List<Boss.Data> pool = new List<Boss.Data>();

    private void Awake()
    {
        JSONNodeReader jsonReader = new JSONNodeReader(fileBosses);
        JSONArrayReader bossesReader = jsonReader.Get<JSONArrayReader>("bosses");
        JSONNodeReader bossNodeReader;
        while (bossesReader.GetNextNode(out bossNodeReader))
        {
            List<Boss.AttackHandler> attacks = new List<Boss.AttackHandler>();
            JSONArrayReader attacksReader = bossNodeReader.Get<JSONArrayReader>("attacks");
            JSONNodeReader attackNodeReader;
            while (attacksReader.GetNextNode(out attackNodeReader))
            {
                string identifier = attackNodeReader.Get("identifier", "ERROR");
                string appearance = attackNodeReader.Get("appearance", "ERROR");
                if (identifier == "FloorSpikes")
                {
                    int count = attackNodeReader.Get("count", 3);
                    GameObject spikeWarning;
                    spikeWarnings.TryGetValue(appearance, out spikeWarning);
                    RuntimeAnimatorController rac;
                    spikeAnimators.TryGetValue(appearance, out rac);
                    FloorSpike.Data d = new FloorSpike.Data(
                        attackNodeReader.Get("damage", 20),
                        spikeWarning,
                        attackNodeReader.Get("seconds of warning", 1.0f),
                        attackNodeReader.Get("seconds of rising", 0.2f),
                        attackNodeReader.Get("seconds to idle", 3.0f),
                        attackNodeReader.Get("seconds to lower", 0.5f),
                        attackNodeReader.Get("height to rise", 1.0f),
                        attackNodeReader.Get("height to rise variance", 0.0f));
                    attacks.Add(x => Boss.FloorSpikes(x, d, count, rac));
                }
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
            }

            Boss.Data boss = new Boss.Data(
                bossNodeReader.Get("identifier", "ERROR"),
                bossNodeReader.Get("base health", 100),
                bossNodeReader.Get("bonus health per arena", 100),
                bossNodeReader.Get("seconds of cooldown between attacks", 2.0f),
                attacks);

            pool.Add(boss);
        }
    }

    public Boss.Data GetRandomBoss()
    {
        return UtilRandom.GetRandomElement(pool);
    }
}