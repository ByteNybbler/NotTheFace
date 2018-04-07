// Author(s): Paul Calande
// Component for attaching a health quantity to a meter.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthToMeter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The health component to interface with.")]
    Health health;
    [SerializeField]
    [Tooltip("The meter component to interface with.")]
    UIMeter meter;

    private void UpdateMeter(float currentValue, float maxValue)
    {
        meter.SetProportion(currentValue, maxValue);
    }

    public void SetHealth(Health health)
    {
        UnsubscribeFromHealth();
        this.health = health;
        SubscribeToHealth();
        UpdateMeter(health.GetCurrentHealth(), health.GetMaxHealth());
    }

    private void SubscribeToHealth()
    {
        if (health != null)
        {
            health.CurrentHealthChanged += Health_CurrentHealthChanged;
            health.MaxHealthChanged += Health_MaxHealthChanged;
        }
    }

    private void UnsubscribeFromHealth()
    {
        if (health != null)
        {
            health.CurrentHealthChanged -= Health_CurrentHealthChanged;
            health.MaxHealthChanged -= Health_MaxHealthChanged;
        }
    }

    private void OnEnable()
    {
        SubscribeToHealth();
    }

    private void OnDisable()
    {
        UnsubscribeFromHealth();
    }

    private void Health_CurrentHealthChanged(int newHealthCurrent)
    {
        UpdateMeter(newHealthCurrent, health.GetMaxHealth());
    }

    private void Health_MaxHealthChanged(int newHealthMax)
    {
        UpdateMeter(health.GetCurrentHealth(), newHealthMax);
    }
}