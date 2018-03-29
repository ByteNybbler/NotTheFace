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
    [Tooltip("Used to check when the player hits the ground to stop headbutts.")]
    GroundChecker2D groundChecker;

    // Whether the player is currently in a headbutt.
    bool headbutting = false;

    protected override void Start()
    {
        base.Start();
        groundChecker.GroundLanded += HeadbuttExit;
    }

    public override void ReceiveInput(InputReader inputReader)
    {
        if (inputReader.GetKeyDown(KeyCode.E))
        {
            tongue.Fire();
        }
        if (inputReader.GetKeyDown(KeyCode.Q))
        {
            HeadbuttEnter();
        }
    }

    private void HeadbuttEnter()
    {
        if (!headbutting && !groundChecker.IsOnGround())
        {
            headbutting = true;
            distributor.UnsubscribeFromInputManager();
        }
    }

    private void HeadbuttExit()
    {
        if (headbutting)
        {
            headbutting = false;
            distributor.SubscribeToInputManager();
        }
    }

    public bool IsHeadbutting()
    {
        return headbutting;
    }
}