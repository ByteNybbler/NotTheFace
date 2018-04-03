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
    [Tooltip("The GameObject to activate when headbutting.")]
    GameObject headbutt;

    // Whether the player is currently in a headbutt.
    bool headbutting = false;

    private void Start()
    {
        groundChecker.GroundLanded += HeadbuttExit;
        headbutt.SetActive(false);
    }

    public void HeadbuttEnter()
    {
        if (!headbutting && !groundChecker.IsOnGround())
        {
            headbutting = true;
            distributor.UnsubscribeFromInputManager();
            headbutt.SetActive(true);
        }
    }

    public void HeadbuttExit()
    {
        if (headbutting)
        {
            headbutting = false;
            distributor.SubscribeToInputManager();
            headbutt.SetActive(false);
        }
    }

    public bool IsHeadbutting()
    {
        return headbutting;
    }
}