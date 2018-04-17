// Author(s): Paul Calande
// Trigger-based application of MonoPeriodicVar to MonoPeriodicVarTimers.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventPeriodicVarTimers2D<TVar, TMonoPeriodicVar,
    TMonoPeriodicVarTimers> : MonoBehaviour
    where TMonoPeriodicVar : MonoPeriodicVar<TVar>
    where TMonoPeriodicVarTimers : MonoPeriodicVarTimers<TVar>
{
    [SerializeField]
    [Tooltip("The trigger event for entering the trigger.")]
    TriggerEvent2D triggerEnter;
    [SerializeField]
    [Tooltip("The trigger event for exiting the trigger.")]
    TriggerEvent2D triggerExit;
    [SerializeField]
    [Tooltip("The timers to populate.")]
    TMonoPeriodicVarTimers timers;

    private void Start()
    {
        if (triggerEnter != null)
        {
            triggerEnter.Subscribe(Enter);
        }
        if (triggerExit != null)
        {
            triggerExit.Subscribe(Exit);
        }
    }

    private void Enter(Collider2D collision)
    {
        TMonoPeriodicVar periodicVar = collision.GetComponent<TMonoPeriodicVar>();
        if (periodicVar != null)
        {
            timers.Add(periodicVar.Get());
        }
    }

    private void Exit(Collider2D collision)
    {
        TMonoPeriodicVar periodicVar = collision.GetComponent<TMonoPeriodicVar>();
        if (periodicVar != null)
        {
            timers.Remove(periodicVar.Get());
        }
    }
}