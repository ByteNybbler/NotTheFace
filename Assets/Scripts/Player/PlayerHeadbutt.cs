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
    [Tooltip("Used for checking the Rigidbody's velocity.")]
    RigidbodyVelocityInUpSpace2D vius;
    [SerializeField]
    [Tooltip("The GameObject to activate when headbutting.")]
    GameObject headbutt;
    [SerializeField]
    [Tooltip("The GameObject that rolls the head like a wheel.")]
    GameObject rbWheel;
    [SerializeField]
    [Tooltip("The GameObject that adjusts the head's heading based on its velocity.")]
    GameObject rbHeading;
    [SerializeField]
    [Tooltip("Detonator explosion that appears when the headbutt state ends.")]
    Instantiator instantiatorDetonator;

    // The minimum horizontal speed required to headbutt.
    float requiredHorizontalSpeed;

    // Whether the player is currently in a headbutt.
    bool headbutting = false;

    private void Start()
    {
        groundChecker.GroundLanded += HeadbuttExit;
        headbutt.SetActive(false);
    }

    private bool HasEnoughHorizontalSpeed()
    {
        return Mathf.Abs(vius.GetVelocityX()) >= requiredHorizontalSpeed;
    }

    private bool ReadyToHeadbutt()
    {
        return !groundChecker.IsOnGround() && HasEnoughHorizontalSpeed();
    }

    public void HeadbuttEnter()
    {
        if (!headbutting && ReadyToHeadbutt())
        {
            headbutting = true;
            distributor.UnsubscribeFromInputManager();
            headbutt.SetActive(true);
            rbWheel.SetActive(false);
            rbHeading.SetActive(true);
        }
    }

    public void HeadbuttExit()
    {
        if (headbutting)
        {
            headbutting = false;
            distributor.SubscribeToInputManager();
            headbutt.SetActive(false);
            rbWheel.SetActive(true);
            rbHeading.SetActive(false);

            instantiatorDetonator.Instantiate();
        }
    }

    public bool IsHeadbutting()
    {
        return headbutting;
    }

    public void SetRequiredHorizontalSpeed(float val)
    {
        requiredHorizontalSpeed = val;
    }
}