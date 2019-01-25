// Author(s): Paul Calande
// Randomly offsets the instantiated object's position within
// a given amount in each direction.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedRandomOffset : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The instantiator to subscribe to.")]
    Instantiator instantiator;
    [SerializeField]
    [Tooltip("The offset radii for the x, y, and z directions, respectively.")]
    Vector3 offsetSizes;

    private void Awake()
    {
        instantiator.Instantiated += Instantiated;
    }

    private void Instantiated(GameObject obj, float secondsOverflow)
    {
        obj.transform.position += new Vector3(
            Random.Range(-offsetSizes.x, offsetSizes.x),
            Random.Range(-offsetSizes.y, offsetSizes.y),
            Random.Range(-offsetSizes.z, offsetSizes.z));
    }
}