// Author(s): Paul Calande
// Destroys a GameObject when a VoidEvent is invoked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToDestroy : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The GameObject to destroy.")]
    GameObject toDestroy;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }

    private void Fire()
    {
        Destroy(toDestroy);
    }
}