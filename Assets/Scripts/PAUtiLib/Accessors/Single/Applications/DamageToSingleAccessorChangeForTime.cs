// Author(s): Paul Calande
// Script that causes a value to change when damage is taken.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToSingleAccessorChangeForTime<TValue, TAccessor, TAccessorChangeForTime>
    : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
    where TAccessorChangeForTime : SingleAccessorChangeForTime<TValue, TAccessor>
{
    [Tooltip("Health component that activates the value change when damaged.")]
    [SerializeField]
    Health health;
    [SerializeField]
    [Tooltip("Temporary value modification component that is utilized when damage is taken.")]
    TAccessorChangeForTime valueChanger;

    private void Start()
    {
        health.Damaged += Health_Damaged;
    }

    private void Health_Damaged(int damage)
    {
        valueChanger.Run();
    }
}