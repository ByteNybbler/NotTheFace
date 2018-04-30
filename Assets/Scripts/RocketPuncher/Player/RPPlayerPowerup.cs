// Author(s): Paul Calande
// Player powerup script for Rocket Puncher.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPPlayerPowerup : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        [Tooltip("How many seconds the Battle Axe powerup lasts.")]
        public float secondsOfBattleAxe;
        [Tooltip("How many seconds the More Arms powerup lasts.")]
        public float secondsOfMoreArms;

        public Data(float secondsOfBattleAxe, float secondsOfMoreArms)
        {
            this.secondsOfBattleAxe = secondsOfBattleAxe;
            this.secondsOfMoreArms = secondsOfMoreArms;
        }
    }
    [SerializeField]
    Data data;

    [SerializeField]
    [Tooltip("Reference to the PlayerPunch component.")]
    RPPlayerPunch playerPunch;
    [SerializeField]
    [Tooltip("Voice clips for collecting a powerup.")]
    SOAAudioClip voicePowerup;
    [SerializeField]
    TimeScale ts;

    AudioController ac;

    // Whether a collectible powerup currently exists in the scene.
    // Is also true if a powerup effect is currently active.
    bool powerupExists = false;

    // Timer for the Battle Axe powerup effect.
    Timer timerBattleAxe;
    Timer timerMoreArms;

    public void SetData(Data val)
    {
        data = val;
    }

    private void Start()
    {
        ac = ServiceLocator.GetAudioController();
        timerBattleAxe = new Timer(data.secondsOfBattleAxe, TimerBattleAxeFinished, false);
        timerMoreArms = new Timer(data.secondsOfMoreArms, TimerMoreArmsFinished, false);
    }

    public bool GetPowerupExists()
    {
        return powerupExists;
    }
    public void SetPowerupExists(bool val)
    {
        powerupExists = val;
    }

    public void GivePowerup(ItemType itemType)
    {
        ac.PlaySFX(voicePowerup.GetRandomElement());
        switch (itemType)
        {
            case ItemType.BattleAxe:
                playerPunch.UseBattleAxe(true);
                timerBattleAxe.Run();
                break;
            case ItemType.MoreArms:
                playerPunch.UseMoreArms(true);
                timerMoreArms.Run();
                break;
        }
    }

    private void TimerBattleAxeFinished(float secondsOverflow)
    {
        playerPunch.UseBattleAxe(false);
        SetPowerupExists(false);
    }

    private void TimerMoreArmsFinished(float secondsOverflow)
    {
        playerPunch.UseMoreArms(false);
        SetPowerupExists(false);
    }

    private void FixedUpdate()
    {
        timerBattleAxe.Tick(ts.DeltaTime());
        timerMoreArms.Tick(ts.DeltaTime());
    }
}