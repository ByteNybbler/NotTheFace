// Author(s): Paul Calande
// Deactivates the GameObject when FixedUpdate runs.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateInFixedUpdate : MonoBehaviour
{
    private void FixedUpdate()
    {
        gameObject.SetActive(false);
    }
}