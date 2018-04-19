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
    PlayerHeadbutt playerHeadbutt;
    [SerializeField]
    PlayerDuck playerDuck;
    [SerializeField]
    Damage damageTongue;
    [SerializeField]
    Damage damageHeadbutt;
    [SerializeField]
    Damage damageLaser;
    [SerializeField]
    InstantiatedDamage damageEggExplosion;
    [SerializeField]
    InstantiatedDamage damageHeadbuttExplosion;
    [SerializeField]
    MonoPeriodicInt damagePerSecondOfContact;
    [SerializeField]
    GroundBasedAcceleration2D gban;
    [SerializeField]
    GroundBasedAccelerator2D gbar;
    [SerializeField]
    GroundBasedJumper2D gbj;
    [SerializeField]
    Gravity2D gravity;
    [SerializeField]
    Health health;
    [SerializeField]
    ActivationForTime invincibilityFrames;
    [SerializeField]
    ActivationDictionary powerupVisuals;

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
        playerHeadbutt.SetRequiredHorizontalSpeed(
            jsonP.Get("headbutt required horizontal speed", 8.0f));
        gbar.SetAcceleration(jsonP.Get("horizontal acceleration", 40.0f));
        gban.SetGroundDeceleration(jsonP.Get("ground deceleration", 32.0f));
        gban.SetMaxHorizontalSpeed(jsonP.Get("max horizontal speed", 16.0f));
        gbj.SetJumpVelocity(jsonP.Get("jump velocity", 20.0f));
        gravity.SetAcceleration(jsonP.Get("gravity", 39.2f));
        InitializeHealth(jsonP.Get("health", 100));
        invincibilityFrames.SetSecondsToChange(jsonP.Get(
            "seconds of invincibility when damaged", 1.0f));
        damagePerSecondOfContact.SetSeconds(1.0f);
    }

    public void AddItemVisualEffect(string identifier)
    {
        powerupVisuals.TrySetActive(identifier, true);
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

    public void AddHorizontalAcceleration(float amount)
    {
        gbar.AddAcceleration(amount);
    }

    public void AddJumpVelocity(float amount)
    {
        gbj.AddJumpVelocity(amount);
    }

    public void AddMaxHorizontalSpeed(float amount)
    {
        gban.AddMaxHorizontalSpeed(amount);
    }

    public void AddLaserDamage(int amount)
    {
        damageLaser.Add(amount);
    }

    public void AddContactDamagePerSecond(int amount)
    {
        damagePerSecondOfContact.AddVar(amount);
    }

    public void AddEggExplosionDamage(int amount)
    {
        damageEggExplosion.Add(amount);
    }

    public void AddHeadbuttExplosionDamage(int amount)
    {
        damageHeadbuttExplosion.Add(amount);
    }

    public void SetHasSoap(bool hasSoap)
    {
        playerDuck.SetHasSoap(hasSoap);
    }
}