// Author(s): Paul Calande
// Sets the trigger's down direction based on the Transform's euler angles.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNewDownDirectionFromTransform2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The trigger's down direction component to assign to.")]
    TriggerNewDownDirection2D dd;

    private void FixedUpdate()
    {
        dd.SetDownDirection(Angle.FromDegrees(transform.rotation.eulerAngles.z));
    }
}