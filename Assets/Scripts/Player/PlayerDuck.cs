using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuck : MonoBehaviour
{
    [SerializeField]
    InputGroundBasedJump2D gbj;
    [SerializeField]
    InputGroundBasedWalk2D gbw;
    [SerializeField]
    GroundChecker2D groundChecker;
    [SerializeField]
    [Tooltip("The soap shield to use when ducking.")]
    GameObject soapShield;
    [SerializeField]
    [Tooltip("GameObject for taking damage to disable when ducking with soap.")]
    GameObject takeDamage;
    [SerializeField]
    [Tooltip("Whether the player has the soap item.")]
    bool hasSoap = false;

    // Whether the player is "ducking" by pressing down.
    bool ducking = false;

    private void Start()
    {
        UpdateSoapShield();
    }

    // Change the ducking state.
    public void SetDucking(bool willDuck)
    {
        // Only change the ducking state if the character is on the ground.
        if (!groundChecker.IsOnGround())
        {
            return;
        }
        ducking = willDuck;
        gbw.SetSubscribedToDistributor(!ducking);
        gbj.SetSubscribedToDistributor(!ducking);
        if (hasSoap)
        {
            UpdateSoapShield();
        }
    }

    private void UpdateSoapShield()
    {
        soapShield.SetActive(ducking);
        takeDamage.SetActive(!ducking);
    }

    public bool GetDucking()
    {
        return ducking;
    }

    public void SetHasSoap(bool hasSoap)
    {
        this.hasSoap = hasSoap;
    }
}