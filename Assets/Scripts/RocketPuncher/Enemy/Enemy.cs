// Author(s): Paul Calande
// Rocket puncher enemy.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class Data : IDeepCopyable<Data>
    {
        [Tooltip("An approximate rating of how difficult the enemy is to deal with.")]
        public float challenge;
        [Tooltip("Oscillation-based enemy movement configuration.")]
        public OscillatePosition2D.Data oscData;
        [Tooltip("Attack configuration.")]
        public EnemyAttack.Data attackData;
        [Tooltip("Sprite configuration.")]
        public EnemySprite.Data spriteData;
        [Tooltip("Left movement data.")]
        public Vector2 leftMovementData;
        [Tooltip("Enemy health data.")]
        public EnemyHealth.Data enemyHealthData;

        public Data(float challenge,
            OscillatePosition2D.Data oscData, EnemyAttack.Data attackData,
            EnemySprite.Data spriteData, Vector2 leftMovementData,
            EnemyHealth.Data enemyHealthData)
        {
            this.challenge = challenge;
            this.oscData = oscData;
            this.attackData = attackData;
            this.spriteData = spriteData;
            this.leftMovementData = leftMovementData;
            this.enemyHealthData = enemyHealthData;
        }

        public Data DeepCopy()
        {
            return new global::Enemy.Data(challenge, oscData.DeepCopy(), attackData.DeepCopy(),
                spriteData.DeepCopy(), leftMovementData,
                enemyHealthData.DeepCopy());
        }
    }

    [SerializeField]
    EnemyAttack enemyAttack;
    [SerializeField]
    Velocity2D leftMovement;
    [SerializeField]
    OscillatePosition2D oscillatePosition2D;
    [SerializeField]
    EnemyHealth enemyHealth;
    [SerializeField]
    EnemySprite enemySprite;

    public void SetData(Data val)
    {
        oscillatePosition2D.SetData(val.oscData);
        leftMovement.SetVelocity(val.leftMovementData);
        enemyAttack.SetData(val.attackData);
        enemySprite.SetData(val.spriteData);
        enemyHealth.SetData(val.enemyHealthData);
    }
}