// Author(s): Paul Calande
// Component for attaching a health quantity to a meter.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthToMeter : MonoBehaviour
{
    [Tooltip("The health component to interface with.")]
    public Health health;
    [Tooltip("The meter component to interface with.")]
    public UIMeter meter;

    private void UpdateMeter(float currentValue, float maxValue)
    {
        meter.SetProportion(currentValue, maxValue);
    }

    private void Start()
    {
        //UpdateMeter(health.GetCurrentHealth(), health.GetMaxHealth());
    }

    private void OnEnable()
    {
        health.CurrentHealthChanged += Health_CurrentHealthChanged;
        health.MaxHealthChanged += Health_MaxHealthChanged;
    }
    private void OnDisable()
    {
        health.CurrentHealthChanged -= Health_CurrentHealthChanged;
        health.MaxHealthChanged -= Health_MaxHealthChanged;
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