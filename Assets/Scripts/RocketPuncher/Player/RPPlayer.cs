// Author(s): Paul Calande
// Script for tuning the player.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPPlayer : MonoBehaviour
{
    public class Data
    {
        public RPPlayerPunch.Data punchData;
        public RPPlayerPowerup.Data powerupData;
        public RPPlayerInput.Data inputData;
        public RPPlayerHealth.Data healthData;
        public RPPlayerDeathTracker.Data deathTrackerData;

        public Data(RPPlayerPunch.Data punchData,
            RPPlayerPowerup.Data powerupData,
            RPPlayerInput.Data inputData,
            RPPlayerHealth.Data healthData,
            RPPlayerDeathTracker.Data deathTrackerData)
        {
            this.punchData = punchData;
            this.powerupData = powerupData;
            this.inputData = inputData;
            this.healthData = healthData;
            this.deathTrackerData = deathTrackerData;
        }
    }

    [SerializeField]
    [Tooltip("File to use for player tuning variables.")]
    TextAsset playerFile;
    [SerializeField]
    [Tooltip("File to use for item tuning variables.")]
    TextAsset itemFile;
    [SerializeField]
    [Tooltip("Reference to the PlayerPunch component.")]
    RPPlayerPunch playerPunch;
    [SerializeField]
    [Tooltip("Reference to the PlayerHealth component.")]
    RPPlayerHealth playerHealth;
    [SerializeField]
    [Tooltip("Reference to the PlayerPowerup component.")]
    RPPlayerPowerup playerPowerup;
    [SerializeField]
    [Tooltip("Reference to the PlayerInput component.")]
    RPPlayerInput playerInput;
    [SerializeField]
    [Tooltip("Reference to the PlayerDeathTracker component.")]
    RPPlayerDeathTracker playerDeathTracker;

    [SerializeField]
    [Tooltip("Voice clips for starting the level.")]
    SOAAudioClip voiceStart;

    AudioController ac;

    public void SetData(Data val)
    {
        playerPunch.SetData(val.punchData);
        playerPowerup.SetData(val.powerupData);
        playerInput.SetData(val.inputData);
        playerHealth.SetData(val.healthData);
        playerDeathTracker.SetData(val.deathTrackerData);
    }

    private void Awake()
    {
        Tune();
    }

    private void Start()
    {
        ac = ServiceLocator.GetAudioController();

        ac.PlaySFX(voiceStart.GetRandomElement());
    }

    private void Tune()
    {
        JSONNodeReader jsonP = new JSONNodeReader(playerFile);
        JSONNodeReader jsonI = new JSONNodeReader(itemFile);
        RPPlayerPunch.Data punchData = new RPPlayerPunch.Data(
            jsonP.Get("seconds of punching", 1.0f),
            jsonP.Get("seconds of punch cooldown", 1.0f),
            jsonI.Get("seconds of more arms punch cooldown", 0.1f),
            jsonP.Get("base punch hitbox size", 1.4f),
            jsonI.Get("battle axe hitbox size", 2.0f));
        RPPlayerPowerup.Data powerupData = new RPPlayerPowerup.Data(
            jsonI.Get("seconds of battle axe", 8.0f),
            jsonI.Get("seconds of more arms", 8.0f));
        RPPlayerInput.Data inputData = new RPPlayerInput.Data(
            jsonP.Get("base movement speed", 10.0f));
        RPPlayerHealth.Data healthData = new RPPlayerHealth.Data(
            jsonP.Get("seconds of invincibility when damaged", 1.0f),
            jsonP.Get("chance of saying fuck", 0.01f),
            jsonP.Get("max health", 100));
        RPPlayerDeathTracker.Data deathData = new RPPlayerDeathTracker.Data(
            jsonP.Get("seconds to wait after dying", 5.0f));
        SetData(new Data(punchData, powerupData, inputData, healthData, deathData));
    }
}