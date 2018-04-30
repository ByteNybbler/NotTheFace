// Author(s): Paul Calande
// Enemy health class for Rocket Puncher.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [System.Serializable]
    public class Data : IDeepCopyable<Data>
    {
        [System.Serializable]
        public class Refs
        {
            public TimeScale ts;
            public RPScore score;
            public RPPlayerPowerup playerPowerup;

            public Refs(TimeScale ts, RPScore score, RPPlayerPowerup playerPowerup)
            {
                this.ts = ts;
                this.score = score;
                this.playerPowerup = playerPowerup;
            }
        }
        public Refs refs;
        [Tooltip("The health kit to drop when dead.")]
        public ItemHealthKit.Data healthKit;
        [Tooltip("How many points the enemy gives when it's killed.")]
        public int pointsWhenKilled;
        [Tooltip("Item drop rates.")]
        public Probability<ItemType> probItem;

        public Data(Refs refs, ItemHealthKit.Data healthKit, int pointsWhenKilled,
            Probability<ItemType> probItem)
        {
            this.refs = refs;
            this.healthKit = healthKit;
            this.pointsWhenKilled = pointsWhenKilled;
            this.probItem = probItem;
        }

        public Data DeepCopy()
        {
            return new Data(refs, healthKit.DeepCopy(),
                pointsWhenKilled, probItem.DeepCopy());
        }
    }
    [SerializeField]
    Data data;

    [SerializeField]
    [Tooltip("Reference to the enemy's left movement component.")]
    Velocity2D leftMovement;
    [SerializeField]
    [Tooltip("The prefab to use to spawn health kits.")]
    GameObject prefabHealthKit;
    [SerializeField]
    [Tooltip("The prefab to use to spawn the Battle Axe powerup.")]
    GameObject prefabBattleAxe;
    [SerializeField]
    [Tooltip("The prefab to use to spawn the More Arms powerup.")]
    GameObject prefabMoreArms;
    [SerializeField]
    [Tooltip("Sounds to play for the enemy dying.")]
    SOAAudioClip enemyDeathSounds;
    [SerializeField]
    [Tooltip("Explosion object to instantiate upon death.")]
    GameObject deathExplosion;

    AudioController ac;

    private void Start()
    {
        ac = ServiceLocator.GetAudioController();
    }

    public void SetData(Data val)
    {
        data = val;
    }

    public void Kill()
    {
        data.refs.score.Add(data.pointsWhenKilled);
        data.refs.score.PunchedEnemy();
        DropItem();
        ac.PlaySFX(enemyDeathSounds.GetRandomElement());

        GameObject de = Instantiate(deathExplosion, transform.position, Quaternion.identity);
        Velocity2D lm = de.GetComponent<Velocity2D>();
        lm.SetVelocity(leftMovement.GetVelocity());
        lm.SetTimeScale(data.refs.ts);

        Destroy(gameObject);
    }

    // Attempt to drop something.
    private void DropItem()
    {
        ItemType it = data.probItem.Roll();
        if (it == ItemType.None)
        {
            return;
        }
        if (it == ItemType.HealthKit)
        {
            GameObject pup = Instantiate(prefabHealthKit, transform.position, Quaternion.identity);
            ItemHealthKit hk = pup.GetComponent<ItemHealthKit>();
            hk.SetData(data.healthKit);

            Velocity2D lm = pup.GetComponent<Velocity2D>();
            lm.SetVelocity(leftMovement.GetVelocity());
            lm.SetTimeScale(data.refs.ts);
        }
        else
        {
            // Only drop these powerups if none currently exist.
            if (!data.refs.playerPowerup.GetPowerupExists())
            {
                GameObject powerup = null;
                switch (it)
                {
                    case ItemType.BattleAxe:
                        powerup = Instantiate(prefabBattleAxe, transform.position, Quaternion.identity);
                        break;
                    case ItemType.MoreArms:
                        powerup = Instantiate(prefabMoreArms, transform.position, Quaternion.identity);
                        break;
                }
                ItemPowerup pup = powerup.GetComponent<ItemPowerup>();
                pup.SetPlayerPowerup(data.refs.playerPowerup);

                Velocity2D lm = powerup.GetComponent<Velocity2D>();
                lm.SetVelocity(leftMovement.GetVelocity());
                lm.SetTimeScale(data.refs.ts);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerPunch"))
        {
            Kill();
        }
    }
}