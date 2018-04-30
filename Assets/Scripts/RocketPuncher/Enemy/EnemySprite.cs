// Author(s): Paul Calande
// Script for managing an enemy's sprite.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySprite : MonoBehaviour
{
    [System.Serializable]
    public class Data : IDeepCopyable<Data>
    {
        [Tooltip("The name of the sprite to use.")]
        public string spriteName;

        public Data (string spriteName)
        {
            this.spriteName = spriteName;
        }

        public Data DeepCopy()
        {
            return new Data(spriteName);
        }
    }
    [SerializeField]
    Data data;

    [SerializeField]
    [Tooltip("The possible sprites to use.")]
    SOKVStringToSprite possibleSprites;
    [SerializeField]
    [Tooltip("Reference to the renderer to use.")]
    SpriteRenderer render;

    public void SetData(Data val)
    {
        data = val;
    }

    private void Start()
    {
        Sprite spr = render.sprite;
        possibleSprites.TryGetValue(data.spriteName, out spr);
        render.sprite = spr;
    }
}