// Author(s): Paul Calande
// Trigger-based application of MonoPeriodicVar to MonoTimedVarTimers.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventTimedVarTimers2D<TVar, TMonoTimedVar,
    TMonoTimedVarTimers> : MonoBehaviour
    where TMonoTimedVar : MonoTimedVar<TVar>
    where TMonoTimedVarTimers : MonoTimedVarTimers<TVar>
{
    [SerializeField]
    [Tooltip("The trigger event for entering the trigger.")]
    TriggerEvent2D triggerEnter;
    [SerializeField]
    [Tooltip("The trigger event for exiting the trigger.")]
    TriggerEvent2D triggerExit;
    [SerializeField]
    [Tooltip("The timers to populate.")]
    TMonoTimedVarTimers timers;

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
        TMonoTimedVar timedVar = collision.GetComponent<TMonoTimedVar>();
        if (timedVar != null)
        {
            timers.Add(timedVar.Get());
        }
    }

    private void Exit(Collider2D collision)
    {
        TMonoTimedVar periodicVar = collision.GetComponent<TMonoTimedVar>();
        if (periodicVar != null)
        {
            timers.Remove(periodicVar.Get());
        }
    }
}