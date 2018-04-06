// Author(s): Paul Calande
// General script for a Not the Face boss.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public delegate void AttackHandler(Boss b);

    [System.Serializable]
    public class Data
    {
        [Tooltip("The unique string that identifies the boss.")]
        public string identifier;
        [Tooltip("How much health the boss would have in the first room.")]
        public int baseHealth;
        [Tooltip("How much health the boss gains per arena.")]
        public int healthBonusPerArena;
        [Tooltip("How many seconds of cooldown occur between each attack.")]
        public float secondsOfCooldownBetweenAttacks;
        [Tooltip("Collection of attacks that the boss uses.")]
        public List<AttackHandler> attacks;

        public Data(string identifier, int baseHealth, int healthBonusPerArena,
            float secondsOfCooldownBetweenAttacks,
            List<AttackHandler> attacks)
        {
            this.identifier = identifier;
            this.baseHealth = baseHealth;
            this.healthBonusPerArena = healthBonusPerArena;
            this.secondsOfCooldownBetweenAttacks = secondsOfCooldownBetweenAttacks;
            this.attacks = attacks;
        }
    }
    [SerializeField]
    Data data;
    [System.Serializable]
    public class Refs
    {
        [Tooltip("The room that the boss is in.")]
        public Room room;

        public Refs(Room room)
        {
            this.room = room;
        }
    }
    [SerializeField]
    Refs refs;

    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    Health health;

    [SerializeField]
    [Tooltip("Prefab to use for floor spikes.")]
    GameObject prefabFloorSpike;

    // Timer for how long the boss waits between attacks.
    Timer timerCooldown;
    // Timer for some actual boss attacks.
    Timer timerAttacking;
    // Collection of attacks that the boss uses.
    //List<AttackHandler> attacks = new List<AttackHandler>();

    public void SetData(Data val)
    {
        data = val;
    }
    public void SetRefs(Refs val)
    {
        refs = val;
    }

    private void Start()
    {
        health.ForceSetBothHealths(data.baseHealth +
            data.healthBonusPerArena * refs.room.GetLoopNumber());
        timerCooldown = new Timer(data.secondsOfCooldownBetweenAttacks,
            CooldownFinished, false, false);
        timerAttacking = new Timer(1.0f, x => timerCooldown.Start(), false, false);
        timerCooldown.Start();
    }

    // Callback function invoked when the cooldown timer is finished.
    private void CooldownFinished(float secondsOverflow)
    {
        // Choose a random attack.
        AttackHandler attack = UtilRandom.GetRandomElement(data.attacks);
        // Execute the attack.
        // The "this" is necessary because the functions are static.
        attack(this);
    }

    private void FixedUpdate()
    {
        float dt = timeScale.DeltaTime();
        timerCooldown.Tick(dt);
        timerAttacking.Tick(dt);
    }

    private void SetAttackTime(float seconds)
    {
        timerAttacking.SetTargetTime(seconds);
        timerAttacking.Start();
    }

    // Summon spikes from the floor.
    public static void FloorSpikes(Boss b, FloorSpike.Data d, int count)
    {
        Room room = b.refs.room;
        for (int i = 0; i < count; ++i)
        {
            GameObject obj = Instantiate(b.prefabFloorSpike, room.transform);
            obj.transform.position = room.GetRandomFloorPosition();
            obj.GetComponent<FloorSpike>().SetData(d);
        }
        b.SetAttackTime(d.secondsOfIdling + d.secondsOfLowering + d.secondsOfRising
            + d.secondsOfWarning);
    }
}