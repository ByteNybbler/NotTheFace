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
    [SerializeField]
    [Tooltip("Prefab to use for projectiles.")]
    GameObject prefabProjectile;

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
    public static void FloorSpikes(Boss b, FloorSpike.Data d, int count,
        RuntimeAnimatorController animator)
    {
        Room room = b.refs.room;
        for (int i = 0; i < count; ++i)
        {
            GameObject obj = Instantiate(b.prefabFloorSpike, room.transform);
            FloorSpike fs = obj.GetComponent<FloorSpike>();
            fs.SetData(d);
            fs.SetAnimatorController(animator);
            float colliderHeight = fs.GetHitboxHeight();
            obj.transform.position = room.GetRandomFloorPosition() +
                Vector3.down * colliderHeight * 0.5f;
        }
        b.SetAttackTime(d.secondsOfIdling + d.secondsOfLowering + d.secondsOfRising
            + d.secondsOfWarning);
    }

    // Fire a projectile.
    public static void FireProjectile(Boss b, Velocity2D.Data d, int damage,
        float spawnHeight, float seconds)
    {
        Room room = b.refs.room;
        GameObject obj = Instantiate(b.prefabProjectile, room.transform);
        obj.transform.position = new Vector3(b.transform.position.x,
            room.GetFloorYPosition() + spawnHeight, 0.0f);
        obj.GetComponent<Velocity2D>().SetData(d);
        obj.GetComponent<Damage>().Add(damage);
        b.SetAttackTime(seconds);
    }
}