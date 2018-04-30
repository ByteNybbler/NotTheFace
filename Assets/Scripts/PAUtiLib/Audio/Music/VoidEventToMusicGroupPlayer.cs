// Author(s): Paul Calande
// Plays music when a VoidEvent is invoked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToMusicGroupPlayer : MusicGroupPlayer
{
    [SerializeField]
    [Tooltip("The void event to subscribe to.")]
    VoidEvent voidEvent;

    private void Awake()
    {
        voidEvent.Subscribe(Play);
    }
}