// Author(s): Paul Calande
// Not the Face player script.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    TextAsset filePlayer;
    [SerializeField]
    PlayerTongue playerTongue;
    [SerializeField]
    Damage damageTongue;
    [SerializeField]
    Damage damageHeadbutt;
    [SerializeField]
    GroundBasedAcceleration2D gban;
    [SerializeField]
    GroundBasedAccelerator2D gbao;
    [SerializeField]
    GroundBasedJumper2D gbj;
    [SerializeField]
    Gravity2D gravity;
    [SerializeField]
    Health health;

    private void Awake()
    {
        Tune();
    }

    private void Tune()
    {
        JSONNodeReader jsonP = new JSONNodeReader(filePlayer);
        playerTongue.SetData(new PlayerTongue.Data(
            jsonP.Get("seconds of tongue", 0.2f),
            jsonP.Get("seconds of tongue cooldown", 1.0f)));
        AddTongueDamage(jsonP.Get("tongue damage", 10));
        AddHeadbuttDamage(jsonP.Get("headbutt damage", 20));
        gbao.SetAcceleration(jsonP.Get("horizontal acceleration", 40.0f));
        gban.SetGroundDeceleration(jsonP.Get("ground deceleration", 32.0f));
        gban.SetMaxHorizontalSpeed(jsonP.Get("max horizontal speed", 16.0f));
        gbj.SetJumpVelocity(jsonP.Get("jump velocity", 20.0f));
        gravity.SetAcceleration(jsonP.Get("gravity", 39.2f));
        InitializeHealth(jsonP.Get("health", 100));
    }

    private void InitializeHealth(int amount)
    {
        health.ForceSetMaxHealth(amount);
        health.ForceSetHealth(amount);
    }

    public void AddMaxHealth(int amount)
    {
        health.AddMaxHealth(amount);
        health.Heal(amount);
    }

    public void AddTongueDamage(int amount)
    {
        damageTongue.Add(amount);
    }

    public void AddHeadbuttDamage(int amount)
    {
        damageHeadbutt.Add(amount);
    }
}