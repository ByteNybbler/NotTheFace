// Author(s): Paul Calande
// ScriptableObject array.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SOAMusicChannelData", order = 10000)]
public class SOAMusicChannelData : ScriptableObject
{
    [SerializeField]
    MusicChannelData[] array;

    public MusicChannelData GetRandomElement()
    {
        return UtilRandom.GetRandomElement(array);
    }

    public int GetLength()
    {
        return array.Length;
    }

    public AudioClip GetClip(int index)
    {
        return array[index].clip;
    }

    public float GetVolume(int index)
    {
        return array[index].volume;
    }
}