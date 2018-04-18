// Author(s): Paul Calande
// Player input script for Not the Face.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : InputDistributed
{
    [SerializeField]
    [Tooltip("Reference to the tongue component.")]
    PlayerTongue tongue;
    [SerializeField]
    [Tooltip("Used to make the player headbutt.")]
    PlayerHeadbutt headbutt;
    [SerializeField]
    [Tooltip("Used to make the player duck.")]
    PlayerDuck duck;
    [SerializeField]
    [Tooltip("Used to make the player fire a laser (if applicable).")]
    PlayerLaser laser;

    public override void ReceiveInput(InputReader inputReader)
    {
        if (inputReader.GetKeyDown(KeyCode.S))
        {
            duck.SetDucking(true);
        }
        if (inputReader.GetKeyUp(KeyCode.S))
        {
            duck.SetDucking(false);
        }
        if (!duck.GetDucking())
        {
            if (inputReader.GetKeyDown(KeyCode.J))
            {
                DoTongueStuff(false);
            }
            if (inputReader.GetKeyDown(KeyCode.K))
            {
                DoTongueStuff(true);
            }
            if (inputReader.GetKeyDown(KeyCode.L))
            {
                if (!tongue.IsTongueRunning())
                {
                    headbutt.HeadbuttEnter();
                }
            }
        }
    }

    private void DoTongueStuff(bool right)
    {
        if (!tongue.IsTongueRunning())
        {
            tongue.Fire(right);
            if (laser.GetDamage() != 0)
            {
                laser.Fire(right);
            }
        }
    }
}