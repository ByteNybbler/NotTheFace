// Author(s): Paul Calande
// Sets some music channel volumes when a VoidEvent is invoked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToMusicChannelVolumeSetter : MusicChannelVolumeSetter
{
    [SerializeField]
    [Tooltip("The void event to subscribe to.")]
    VoidEvent voidEvent;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }
}