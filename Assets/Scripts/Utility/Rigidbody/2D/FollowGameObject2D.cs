// Author(s): Paul Calande
// Constantly tries to move this GameObject to the position of the given GameObject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject2D : MonoBehaviour
{
    [SerializeField]
    Mover2D mover;
    [SerializeField]
    [Tooltip("The GameObject to follow.")]
    GameObject followThis;
    [SerializeField]
    [Tooltip("The positional offset that this GameObject should have from the followed GameObject.")]
    Vector3 offset;

    private void FixedUpdate()
    {
        mover.TeleportPosition(followThis.transform.position + offset);
    }
}