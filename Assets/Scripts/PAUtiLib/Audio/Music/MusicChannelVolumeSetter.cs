// Author(s): Paul Calande
// Sets the volumes of the given music channels.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChannelVolumeSetter : MonoBehaviour
{
    [System.Serializable]
    public class ChannelAndVolume
    {
        public int channel;
        public float volume;
    }

    [SerializeField]
    [Tooltip("The volumes to set the given channels to.")]
    ChannelAndVolume[] channelsAndVolumes;
    [SerializeField]
    [Tooltip("How many seconds the channels take to fade.")]
    float secondsToFade;

    // The AudioController that has the music channels to modify.
    AudioController ac;

    private void Awake()
    {
        ac = ServiceLocator.GetAudioController();
    }

    // Sets the volumes of the given music channels.
    public void Fire()
    {
        foreach (ChannelAndVolume channelAndVolume in channelsAndVolumes)
        {
            ac.SetMusicChannelVolume(channelAndVolume.channel, channelAndVolume.volume,
                secondsToFade);
        }
    }
}