// Author(s): Paul Calande
// Script that makes the AudioController play music on Start.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartToMusicGroupPlayer : MusicGroupPlayer
{
    private void Start()
    {
        Play();
    }
}