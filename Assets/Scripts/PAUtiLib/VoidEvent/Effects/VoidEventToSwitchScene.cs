// Author(s): Paul Calande
// Switches scenes when a VoidEvent is invoked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToSwitchScene : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The new scene to switch to.")]
    string newScene;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }

    private void Fire()
    {
        ServiceLocator.GetSceneTracker().SwitchScene(newScene);
    }
}