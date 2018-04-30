// Author(s): Paul Calande
// Data about a single music channel for a music group.
// This is only used to initialize music groups.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicChannelData
{
    [Tooltip("The clip to use for the music channel.")]
    public AudioClip clip;
    [Tooltip("The initial target volume for the music channel.")]
    public float volume;
}