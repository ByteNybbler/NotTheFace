// Author(s): Paul Calande
// Assigns an instantiated object's SpriteAccessor a random sprite.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedRandomSpriteAccessorSprite : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The instantiator to subscribe to.")]
    Instantiator instantiator;
    [SerializeField]
    [Tooltip("The possible sprites to choose from.")]
    SOASprite sprites;

    private void Awake()
    {
        instantiator.Instantiated += Instantiated;
    }

    private void Instantiated(GameObject obj, float secondsOverflow)
    {
        obj.GetComponent<SpriteAccessor>().Set(sprites.GetRandomElement());
    }
}