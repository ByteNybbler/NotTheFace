// Author(s): Paul Calande
// Script for enemy attacks.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [System.Serializable]
    public class Data : IDeepCopyable<Data>
    {
        [System.Serializable]
        public class Refs
        {
            public TimeScale ts;
            [Tooltip("Reference to the player.")]
            public GameObject player;

            public Refs(TimeScale ts, GameObject player)
            {
                this.ts = ts;
                this.player = player;
            }
        }
        public Refs refs;
        [Tooltip("The volley that the enemy should fire.")]
        public VolleyData volley;
        [Tooltip("How many seconds should pass between each volley.")]
        public float secondsBetweenVolleys;
        [Tooltip("The change in volley direction between each shot.")]
        public float volleyDirectionDeltaPerShot;
        [Tooltip("The left speed that will be applied to fired projectiles.")]
        public float projectileLeftSpeed;

        public Data(Refs refs,
            VolleyData volley,
            float secondsBetweenVolleys,
            float volleyDirectionDeltaPerShot,
            float projectileLeftSpeed)
        {
            this.refs = refs;
            this.volley = volley;
            this.secondsBetweenVolleys = secondsBetweenVolleys;
            this.volleyDirectionDeltaPerShot = volleyDirectionDeltaPerShot;
            this.projectileLeftSpeed = projectileLeftSpeed;
        }

        public Data DeepCopy()
        {
            return new Data(refs,
                volley,
                secondsBetweenVolleys,
                volleyDirectionDeltaPerShot,
                projectileLeftSpeed);
        }
    }
    [SerializeField]
    Data data;

    [SerializeField]
    [Tooltip("Prefab to use for the projectile.")]
    GameObject prefabProjectile;

    // Timer for firing volleys.
    Timer timerVolley;

    public void SetData(Data val)
    {
        data = val;
    }

    private void Start()
    {
        timerVolley = new Timer(data.secondsBetweenVolleys);
        timerVolley.Run();
    }

    private void TimerVolleyFinished(float secondsOverflow)
    {
        VolleyData volley = data.volley;
        float[] angles = UtilSpread.PopulateAngle(volley.spreadAngle,
            data.volley.projectile.angle,
            volley.projectileCount);
        foreach (float a in angles)
        {
            float angle = a;
            if (data.refs.player != null)
            {
                if (volley.aimAtPlayer)
                {
                    Vector3 playerPos = data.refs.player.transform.position;
                    angle += UtilHeading2D.SignedAngleToPoint(transform.position, playerPos);
                }
            }
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject projectile = Instantiate(prefabProjectile, transform.position, rotation);
            EnemyProjectile proj = projectile.GetComponent<EnemyProjectile>();
            EnemyProjectile.Data projData = data.volley.projectile.DeepCopy();
            projData.angle = angle;
            proj.SetData(projData);
            Velocity2D leftMovement = projectile.GetComponent<Velocity2D>();
            leftMovement.SetVelocity(new Vector2(-data.projectileLeftSpeed, 0.0f));
            leftMovement.SetTimeScale(data.refs.ts);
        }
        data.volley.projectile.angle += data.volleyDirectionDeltaPerShot;
    }

    private void FixedUpdate()
    {
        timerVolley.Tick(data.refs.ts.DeltaTime());
    }
}