// Author(s): Paul Calande
// Marks this GameObject as the ServiceLocator's player object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocatorPlayer : MonoBehaviour
{
    private void Start()
    {
        ServiceLocator.SetPlayer(gameObject);
    }
}