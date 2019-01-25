// Author(s): Paul Calande
// Script for making a timer trigger an instantiator each time it finishes.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTimerFinishedToInstantiator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The instantiator to fire.")]
    Instantiator instantiator;
    [SerializeField]
    [Tooltip("The timer that will fire the instantiator each time it finishes.")]
    MonoTimer timer;

    private void Start()
    {
        timer.SubscribeToFinished(instantiator.Instantiate);
    }
}