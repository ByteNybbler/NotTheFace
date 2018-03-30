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
    GroundBasedAcceleration2D gba;
    [SerializeField]
    GroundBasedJumper2D gbj;

    private void Awake()
    {
        JSONNodeReader jsonP = new JSONNodeReader(filePlayer);
        playerTongue.SetData(new PlayerTongue.Data(
            jsonP.Get("seconds of tongue", 0.2f),
            jsonP.Get("seconds of tongue cooldown", 1.0f)));
        damageTongue.Add(jsonP.Get("tongue damage", 10));
        damageHeadbutt.Add(jsonP.Get("headbutt damage", 20));
        gba.SetGroundDeceleration(jsonP.Get("ground deceleration", 32.0f));
        gba.SetMaxHorizontalSpeed(jsonP.Get("max horizontal speed", 16.0f));
        gbj.SetJumpVelocity(jsonP.Get("jump velocity", 20.0f));
    }
}