// Author(s): Paul Calande
// Data for a volley.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VolleyData
{
    [Tooltip("The projectile to fire.")]
    public EnemyProjectile.Data projectile;
    /*
    [Tooltip("The speed of the fired volleys.")]
    public float speed;
    [Tooltip("The direction of the fired volleys.")]
    public float direction;
    */
    [Tooltip("How many projectiles are spawned per volley.")]
    public int projectileCount;
    [Tooltip("The spread of projectiles (in degrees) across one volley.")]
    public float spreadAngle;
    [Tooltip("Whether the volley is aimed at the player.")]
    public bool aimAtPlayer;

    public VolleyData(EnemyProjectile.Data projectile, int projectileCount,
        float spreadAngle, bool aimAtPlayer)
    {
        this.projectile = projectile;
        this.projectileCount = projectileCount;
        this.spreadAngle = spreadAngle;
        this.aimAtPlayer = aimAtPlayer;
    }
}