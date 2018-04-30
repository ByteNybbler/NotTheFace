// Author(s): Paul Calande
// Health kit item for Rocket Puncher.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealthKit : MonoBehaviour
{
    [System.Serializable]
    public class Data : IDeepCopyable<Data>
    {
        [System.Serializable]
        public class Refs
        {
            public RPScore score;

            public Refs(RPScore score)
            {
                this.score = score;
            }
        }
        public Refs refs;
        [Tooltip("How much the health kit heals.")]
        public int heal;
        [Tooltip("How many points the health kit gives when the player is at full health.")]
        public int pointsPerFullHealthHealthKit;

        public Data(Refs refs, int heal, int pointsPerFullHealthHealthKit)
        {
            this.refs = refs;
            this.heal = heal;
            this.pointsPerFullHealthHealthKit = pointsPerFullHealthHealthKit;
        }

        public Data DeepCopy()
        {
            return new Data(refs, heal, pointsPerFullHealthHealthKit);
        }
    }
    [SerializeField]
    Data data;

    public void SetData(Data val)
    {
        data = val;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSelfHitbox"))
        {
            Transform root = collision.transform.root;
            RPPlayerHealth playerHealth = root.GetComponent<RPPlayerHealth>();
            Health health = root.GetComponent<Health>();
            if (health.IsHealthFull())
            {
                // Award points.
                data.refs.score.Add(data.pointsPerFullHealthHealthKit);
            }
            else
            {
                playerHealth.Heal(data.heal);
            }
            playerHealth.HealAudio();
            Destroy(gameObject);
        }
    }
}