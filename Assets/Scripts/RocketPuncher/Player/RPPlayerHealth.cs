// Author(s): Paul Calande
// Script for player health in Rocket Puncher.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPPlayerHealth : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        [Tooltip("How many seconds the player remains invincible when damaged.")]
        public float secondsOfInvincibilityWhenDamaged;
        [Tooltip("The chance that Rocket Puncher has to say fuck when damaged.")]
        public float chanceOfSayingFuck;
        [Tooltip("The maximum health that the player should have.")]
        public int maxHealth;

        public Data(float secondsOfInvincibilityWhenDamaged,
            float chanceOfSayingFuck, int maxHealth)
        {
            this.secondsOfInvincibilityWhenDamaged = secondsOfInvincibilityWhenDamaged;
            this.chanceOfSayingFuck = chanceOfSayingFuck;
            this.maxHealth = maxHealth;
        }
    }
    [SerializeField]
    Data data;

    [SerializeField]
    [Tooltip("Reference to the Health component.")]
    Health health;
    [SerializeField]
    [Tooltip("Reference to the component for coloring the sprites in the hierarchy.")]
    ColorAccessor spriteColorer;
    [SerializeField]
    [Tooltip("The color the player uses when damaged.")]
    Color colorDamaged;
    [SerializeField]
    [Tooltip("The player hurt voice clips.")]
    SOAAudioClip hurtVoiceClips;
    [SerializeField]
    [Tooltip("FUCK")]
    AudioClip fuck;
    [SerializeField]
    [Tooltip("The player heal voice clips.")]
    SOAAudioClip healVoiceClips;
    [SerializeField]
    [Tooltip("The explosion object to instantiate when dying.")]
    GameObject deathExplosion;
    [SerializeField]
    [Tooltip("Reference to the PlayerDeathTracker component.")]
    RPPlayerDeathTracker playerDeathTracker;
    [SerializeField]
    [Tooltip("Array of possible death explosion sounds.")]
    SOAAudioClip deathExplosionSounds;

    // Invincibility timer.
    Timer timerInvincible;
    // Probability machine for whether Rocket Puncher will say fuck.
    Probability<bool> playerSaysFuck = new Probability<bool>(false);

    AudioController ac;

    public void SetData(Data val)
    {
        data = val;
    }

    private void Start()
    {
        ac = ServiceLocator.GetAudioController();
        //health.SetMaxHealth(data.maxHealth);
        //health.FullHeal();
        health.ForceSetMaxHealth(data.maxHealth);
        health.ForceSetHealth(data.maxHealth);
        health.Died += Health_Died;
        timerInvincible = new Timer(data.secondsOfInvincibilityWhenDamaged,
            TimerInvincibleFinished, false);
        playerSaysFuck.SetChance(true, data.chanceOfSayingFuck);
    }

    private void TimerInvincibleFinished(float secondsOverflow)
    {
        MakeVincible();
    }

    private void FixedUpdate()
    {
        timerInvincible.Tick(Time.deltaTime);
    }

    private void Health_Died()
    {
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
        ac.PlaySFX(deathExplosionSounds.GetRandomElement());
        playerDeathTracker.PlayerHasDied();
        Destroy(gameObject);
    }

    // Returns whether the player can currently be damaged.
    private bool IsInvincible()
    {
        return timerInvincible.IsRunning();
    }

    // Play damage sounds.
    private void DamageAudio()
    {
        bool sayFuck = playerSaysFuck.Roll();
        if (sayFuck)
        {
            ac.PlaySFX(fuck);
        }
        else
        {
            ac.PlaySFX(hurtVoiceClips.GetRandomElement());
        }
    }

    public void Damage(int amount)
    {
        if (!IsInvincible())
        {
            health.Damage(amount);
            MakeInvincible();
            SetSpriteColor(colorDamaged);
            DamageAudio();
        }
    }

    public void HealAudio()
    {
        ac.PlaySFX(healVoiceClips.GetRandomElement());
    }

    public void Heal(int amount)
    {
        health.Heal(amount);
    }

    private void MakeVincible()
    {
        SetSpriteColor(Color.white);
    }

    private void MakeInvincible()
    {
        timerInvincible.Run();
    }

    private void SetSpriteColor(Color col)
    {
        spriteColorer.Set(col);
    }
}