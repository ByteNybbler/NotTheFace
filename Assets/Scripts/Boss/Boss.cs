// Author(s): Paul Calande
// General script for a Not the Face boss.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public delegate void AttackHandler(Boss b);

    public class AttackGroup
    {
        // How many seconds of cooldown occur between each attack.
        float secondsOfCooldown;
        // Variance in the seconds of cooldown between each attack.
        float secondsOfCooldownVariance;
        // Collection of attacks in this attack group.
        List<AttackHandler> attacks = new List<AttackHandler>();
        // Timer for how long the boss waits between attacks.
        Timer timerCooldown;
        // The boss that this attack group belongs to.
        Boss boss;

        public AttackGroup(float secondsOfCooldown,
            float secondsOfCooldownVariance)
        {
            this.secondsOfCooldown = secondsOfCooldown;
            this.secondsOfCooldownVariance = secondsOfCooldownVariance;

            timerCooldown = new Timer(1.0f, CooldownFinished, false, false);
        }

        public void AddAttack(AttackHandler attack)
        {
            attacks.Add(attack);
        }

        public void SetBoss(Boss boss)
        {
            this.boss = boss;
        }

        public void Tick(float deltaTime)
        {
            timerCooldown.Tick(deltaTime);
        }

        public void StartCooldown()
        {
            timerCooldown.SetTargetTime(UtilRandom.RangeWithCenter(secondsOfCooldown,
                secondsOfCooldownVariance));
            timerCooldown.Start();
        }

        private AttackHandler GetRandomAttack()
        {
            return UtilRandom.GetRandomElement(attacks);
        }

        // Callback function invoked when the cooldown timer is finished.
        private void CooldownFinished(float secondsOverflow)
        {
            // Choose a random attack.
            AttackHandler attack = UtilRandom.GetRandomElement(attacks);
            // Execute the attack.
            attack(boss);
        }
    }

    [System.Serializable]
    public class Data
    {
        [Tooltip("The unique string that identifies the boss.")]
        public string identifier;
        [Tooltip("How much health the boss would have in the first room.")]
        public int baseHealth;
        [Tooltip("How much health the boss gains per arena.")]
        public int healthBonusPerArena;
        [Tooltip("Collections of attacks that the boss uses.")]
        public List<AttackGroup> attackGroups;

        public Data(string identifier, int baseHealth, int healthBonusPerArena,
            List<AttackGroup> attackGroups)
        {
            this.identifier = identifier;
            this.baseHealth = baseHealth;
            this.healthBonusPerArena = healthBonusPerArena;
            this.attackGroups = attackGroups;
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
    [Tooltip("String identifiers mapped to Transforms.")]
    DictionaryTransform transforms;

    [SerializeField]
    [Tooltip("Prefab to use for floor spikes.")]
    GameObject prefabFloorSpike;

    [SerializeField]
    [Tooltip("Prefab to use for orbs.")]
    GameObject prefabOrb;

    [SerializeField]
    [Tooltip("Prefab to use for projectiles.")]
    GameObject prefabProjectile;

    // Timer for how long the boss waits between attacks.
    //Timer timerCooldown;
    // Timer for some actual boss attacks.
    //Timer timerAttacking;
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

        //timerAttacking = new Timer(1.0f, x => timerCooldown.Start(), false, false);
        //timerCooldown.Start();

        foreach (AttackGroup attackGroup in data.attackGroups)
        {
            attackGroup.SetBoss(this);
            attackGroup.StartCooldown();
        }
    }

    private void FixedUpdate()
    {
        float dt = timeScale.DeltaTime();
        foreach (AttackGroup attackGroup in data.attackGroups)
        {
            attackGroup.Tick(dt);
        }
    }

    /*
    private void SetAttackTime(float seconds)
    {
        timerAttacking.SetTargetTime(seconds);
        timerAttacking.Start();
    }
    */

    // Summon spikes from the floor.
    public static void FloorSpikes(Boss b, AttackGroup a, FloorSpike.Data d, int count,
        RuntimeAnimatorController animator)
    {
        Room room = b.refs.room;
        for (int i = 0; i < count; ++i)
        {
            GameObject obj = Instantiate(b.prefabFloorSpike, b.transform);
            FloorSpike fs = obj.GetComponent<FloorSpike>();
            fs.SetData(d);
            fs.SetAnimatorController(animator);
            float colliderHeight = fs.GetHitboxHeight();
            obj.transform.position = room.GetRandomFloorPosition() +
                Vector3.down * colliderHeight * 0.5f;
        }
        a.StartCooldown();
        /*
        b.SetAttackTime(d.secondsOfIdling + d.secondsOfLowering + d.secondsOfRising
            + d.secondsOfWarning);
            */
    }

    public static void SpawnOrb(Boss b, AttackGroup a, BossOrb.Data d, string positionName,
        //string centerName, string bottomName,
        RuntimeAnimatorController animator)
    {
        Transform spawnLocation, center, bottom;
        b.transforms.TryGetValue(positionName, out spawnLocation);
        //b.transforms.TryGetValue(centerName, out center);
        //b.transforms.TryGetValue(bottomName, out bottom);
        b.transforms.TryGetValue("CenterTop", out center);
        b.transforms.TryGetValue("CenterBottom", out bottom);
        GameObject obj = Instantiate(b.prefabOrb, spawnLocation);
        obj.transform.localPosition = Vector3.zero;
        BossOrb orb = obj.GetComponent<BossOrb>();
        orb.SetData(d);
        orb.SetCenterAndBottom(center.position, bottom.position);
        orb.SetAnimatorController(animator);
        orb.IdleFinished += a.StartCooldown;
    }

    // Fire a projectile.
    /*
    public static void FireProjectile(Boss b, AttackGroup a, Velocity2D.Data d, int damage,
        float spawnHeight)
    {
        Room room = b.refs.room;
        GameObject obj = Instantiate(b.prefabProjectile, room.transform);
        obj.transform.position = new Vector3(b.transform.position.x,
            room.GetFloorYPosition() + spawnHeight, 0.0f);
        obj.GetComponent<Velocity2D>().SetData(d);
        obj.GetComponent<Damage>().Add(damage);
        a.StartCooldown();
    }
    */
}