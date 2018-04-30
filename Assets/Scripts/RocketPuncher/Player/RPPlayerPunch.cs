// Author(s): Paul Calande
// Script for the player's punching mechanic in Rocket Puncher.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPPlayerPunch : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        [Tooltip("How many seconds the punch lasts for.")]
        public float secondsOfPunching;
        [Tooltip("How many seconds of cooldown occur after the end of the punch before a punch can happen again.")]
        public float secondsOfPunchCooldown;
        [Tooltip("Punch cooldown during More Arms powerup.")]
        public float secondsOfMoreArmsPunchCooldown;
        [Tooltip("The scale of the base punch's hitbox.")]
        public float basePunchHitboxSize;
        [Tooltip("The scale of the Battle Axe hitbox.")]
        public float battleAxeHitboxSize;

        public Data(float secondsOfPunching, float secondsOfPunchCooldown,
            float secondsOfMoreArmsPunchCooldown,
            float basePunchHitboxSize, float battleAxeHitboxSize)
        {
            this.secondsOfPunching = secondsOfPunching;
            this.secondsOfPunchCooldown = secondsOfPunchCooldown;
            this.secondsOfMoreArmsPunchCooldown = secondsOfMoreArmsPunchCooldown;
            this.basePunchHitboxSize = basePunchHitboxSize;
            this.battleAxeHitboxSize = battleAxeHitboxSize;
        }
    }
    [SerializeField]
    Data data;

    [SerializeField]
    [Tooltip("The GameObject to use for punching.")]
    GameObject punchingObject;
    [SerializeField]
    [Tooltip("The punching object child to use for regular punching.")]
    GameObject childRegularPunch;
    [SerializeField]
    [Tooltip("The punching object child to use for the Battle Axe powerup.")]
    GameObject childBattleAxe;
    [SerializeField]
    [Tooltip("Voice clips for punching.")]
    SOAAudioClip punchVoiceClips;
    [SerializeField]
    [Tooltip("The GameObject to enable when enabling More Arms.")]
    GameObject moreArmsObject;
    [SerializeField]
    [Tooltip("References to the arms to animate.")]
    Animator[] arms;
    [SerializeField]
    [Tooltip("References to the battle axe objects to animate.")]
    GameObject[] axes;
    [SerializeField]
    [Tooltip("Reference to the punch cooldown meter.")]
    UIMeter meterPunchCooldown;
    [SerializeField]
    [Tooltip("Reference to the punch cooldown meter's visibility component.")]
    UIMeterVisibility meterPunchCooldownVisibility;
    [SerializeField]
    TimeScale ts;

    AudioController ac;
    static int hashPunch = Animator.StringToHash("Punch");
    Timer timerPunching;
    Timer timerPunchCooldown;

    // How many times the player has punched.
    int punchCount = 0;

    public void SetData(Data val)
    {
        data = val;
    }

    private void Start()
    {
        ac = ServiceLocator.GetAudioController();
        punchingObject.SetActive(false);

        childRegularPunch.transform.localScale = new Vector3(data.basePunchHitboxSize,
            data.basePunchHitboxSize, 1.0f);
        childBattleAxe.transform.localScale = new Vector3(data.battleAxeHitboxSize,
            data.battleAxeHitboxSize, 1.0f);

        timerPunching = new Timer(data.secondsOfPunching, TimerPunchingFinished, false);
        timerPunchCooldown = new Timer(data.secondsOfPunchCooldown, null, false);

        UseBattleAxe(false);
        UseMoreArms(false);
    }

    public void UseBattleAxe(bool willUseAxe)
    {
        childRegularPunch.SetActive(!willUseAxe);
        childBattleAxe.SetActive(willUseAxe);
        foreach (GameObject axe in axes)
        {
            axe.SetActive(willUseAxe);
        }
    }

    public void UseMoreArms(bool willUseMoreArms)
    {
        float cooldown;
        if (willUseMoreArms)
        {
            cooldown = data.secondsOfMoreArmsPunchCooldown;
        }
        else
        {
            cooldown = data.secondsOfPunchCooldown;
        }
        timerPunchCooldown.SetTargetTime(cooldown);
        moreArmsObject.SetActive(willUseMoreArms);
        meterPunchCooldownVisibility.SetForcedInvisible(willUseMoreArms);
    }

    // Returns true if the player currently has the Battle Axe powerup.
    public bool HasBattleAxe()
    {
        return childBattleAxe.activeSelf;
    }

    // Returns true if the player currently has the More Arms powerup.
    public bool HasMoreArms()
    {
        return timerPunchCooldown.GetTargetTime() == data.secondsOfMoreArmsPunchCooldown;
    }

    private void AnimateArm()
    {
        int armIndex;
        if (HasMoreArms())
        {
            armIndex = punchCount % 4;
        }
        else
        {
            armIndex = punchCount % 2;
        }
        arms[armIndex].SetTrigger(hashPunch);
    }

    public void Punch()
    {
        if (!IsPunching() && !IsPunchCoolingDown())
        {
            ++punchCount;
            punchingObject.SetActive(true);
            ac.PlaySFX(punchVoiceClips.GetRandomElement());
            AnimateArm();
            timerPunching.Run();
        }
    }

    // Returns true if the player is punching.
    private bool IsPunching()
    {
        return timerPunching.IsRunning();
    }

    // Returns true during the punch cooldown.
    private bool IsPunchCoolingDown()
    {
        return timerPunchCooldown.IsRunning();
    }

    private void TimerPunchingFinished(float secondsOverflow)
    {
        // Punch finished.
        punchingObject.SetActive(false);
        timerPunchCooldown.Run();
    }

    private void FixedUpdate()
    {
        meterPunchCooldown.SetPercent(timerPunchCooldown.GetPercentFinished());
        timerPunching.Tick(ts.DeltaTime());
        timerPunchCooldown.Tick(ts.DeltaTime());
    }
}