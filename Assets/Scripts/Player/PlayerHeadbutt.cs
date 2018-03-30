// Author(s): Paul Calande
// Player headbutt script for Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadbutt : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the player's InputDistributor.")]
    InputDistributor distributor;
    [SerializeField]
    [Tooltip("Used to check when the player hits the ground to stop headbutts.")]
    GroundChecker2D groundChecker;
    [SerializeField]
    [Tooltip("The headbutt's Damage component.")]
    Damage damage;

    // Whether the player is currently in a headbutt.
    bool headbutting = false;

    private void Start()
    {
        groundChecker.GroundLanded += HeadbuttExit;
    }

    public void HeadbuttEnter()
    {
        if (!headbutting && !groundChecker.IsOnGround())
        {
            headbutting = true;
            distributor.UnsubscribeFromInputManager();
        }
    }

    public void HeadbuttExit()
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

    public int GetDamage()
    {
        return damage.Get();
    }
}