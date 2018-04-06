// Author(s): Paul Calande
// Taking damage causes a GameObject to be disabled for some time.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToDisableForTime : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The health component to watch for damage on.")]
    Health health;
    [SerializeField]
    [Tooltip("The component that will disable and enable the GameObject.")]
    DisableForTime disableForTime;

    private void Start()
    {
        health.Damaged += x => disableForTime.Run();
    }
}