// Author(s): Paul Calande
// General script to be attached to powerup items in Rocket Puncher.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPowerup : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The powerup type.")]
    ItemType powerupType;
    [SerializeField]
    [Tooltip("Reference to the player's powerup-tracking component.")]
    RPPlayerPowerup playerPowerup;

    bool collectedByPlayer = false;

    public void SetPlayerPowerup(RPPlayerPowerup val)
    {
        playerPowerup = val;
        NotifyPowerupExists();
    }

    // Notify the player that a powerup exists in the scene.
    private void NotifyPowerupExists()
    {
        playerPowerup.SetPowerupExists(true);
    }

    // The powerup no longer exists, so notify the player that there are no powerups left.
    private void OnDestroy()
    {
        if (playerPowerup != null && !collectedByPlayer)
        {
            playerPowerup.SetPowerupExists(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSelfHitbox"))
        {
            playerPowerup.GivePowerup(powerupType);
            collectedByPlayer = true;
            Destroy(gameObject);
        }
    }
}