// Author(s): Paul Calande
// Health script.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The maximum health the object can have.")]
    private int healthMax;
    [SerializeField]
    [Tooltip("The current health the object has.")]
    private int healthCurrent;

    public delegate void DiedHandler();
    public event DiedHandler Died;
    public delegate void HealedHandler(int amount);
    public event HealedHandler Healed;
    public delegate void DamagedHandler(int amount);
    public event DamagedHandler Damaged;
    public delegate void MaxHealthAddedHandler(int amount);
    public event MaxHealthAddedHandler MaxHealthAdded;
    public delegate void MaxHealthSubtractedHandler(int amount);
    public event MaxHealthSubtractedHandler MaxHealthSubtracted;
    public delegate void CurrentHealthChangedHandler(int newHealthCurrent);
    public event CurrentHealthChangedHandler CurrentHealthChanged;
    public delegate void MaxHealthChangedHandler(int newHealthMax);
    public event MaxHealthChangedHandler MaxHealthChanged;

    public int GetCurrentHealth()
    {
        return healthCurrent;
    }

    public int GetMaxHealth()
    {
        return healthMax;
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            Heal(-amount);
        }
        else if (!IsDead() && amount != 0)
        {
            amount = Mathf.Min(amount, healthCurrent);
            healthCurrent -= amount;
            OnDamaged(amount);
            OnCurrentHealthChanged(healthCurrent);
            if (IsDead())
            {
                OnDied();
            }
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            Damage(-amount);
        }
        else if (!IsHealthFull() && amount != 0)
        {
            amount = Mathf.Min(amount, healthMax - healthCurrent);
            healthCurrent += amount;
            OnHealed(amount);
            OnCurrentHealthChanged(healthCurrent);
        }
    }

    public void SetHealth(int amount)
    {
        if (amount > healthCurrent)
        {
            Heal(amount - healthCurrent);
        }
        if (amount < healthCurrent)
        {
            Damage(healthCurrent - amount);
        }
    }

    // Restore the current health to the max health.
    public void FullHeal()
    {
        Heal(healthMax - healthCurrent);
    }

    // Effectively set the health to 0, causing death.
    public void Die()
    {
        Damage(healthCurrent);
    }

    public void AddMaxHealth(int amount)
    {
        healthMax += amount;
        OnMaxHealthAdded(amount);
        OnMaxHealthChanged(healthMax);
    }

    public void SubtractMaxHealth(int amount)
    {
        healthMax -= amount;
        OnMaxHealthSubtracted(amount);
        OnMaxHealthChanged(healthMax);
        if (healthCurrent > healthMax)
        {
            Damage(healthCurrent - healthMax);
        }
    }

    public void SetMaxHealth(int amount)
    {
        if (amount > healthMax)
        {
            AddMaxHealth(amount - healthMax);
        }
        if (amount < healthMax)
        {
            SubtractMaxHealth(healthMax - amount);
        }
    }

    public bool IsHealthFull()
    {
        return (healthCurrent == healthMax);
    }

    // Returns true if there's no health left.
    public bool IsDead()
    {
        return (healthCurrent <= 0);
    }

    // Sets the health without restraints and without invoking any events.
    public void ForceSetHealth(int amount)
    {
        healthCurrent = amount;
    }
    public void ForceSetMaxHealth(int amount)
    {
        healthMax = amount;
    }
    public void ForceSetBothHealths(int amount)
    {
        healthMax = amount;
        healthCurrent = amount;
    }

    // Event invocations.
    private void OnDied()
    {
        if (Died != null)
        {
            Died();
        }
    }
    private void OnHealed(int amount)
    {
        if (Healed != null)
        {
            Healed(amount);
        }
    }
    private void OnDamaged(int amount)
    {
        if (Damaged != null)
        {
            Damaged(amount);
        }
    }
    private void OnMaxHealthAdded(int amount)
    {
        if (MaxHealthAdded != null)
        {
            MaxHealthAdded(amount);
        }
    }
    private void OnMaxHealthSubtracted(int amount)
    {
        if (MaxHealthSubtracted != null)
        {
            MaxHealthSubtracted(amount);
        }
    }
    private void OnCurrentHealthChanged(int newHealthCurrent)
    {
        if (CurrentHealthChanged != null)
        {
            CurrentHealthChanged(newHealthCurrent);
        }
    }
    private void OnMaxHealthChanged(int newHealthMax)
    {
        if (MaxHealthChanged != null)
        {
            MaxHealthChanged(newHealthMax);
        }
    }
}