// Author(s): Paul Calande
// Moves this GameObject to the position of the given GameObject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to follow.")]
    GameObject followThis;
    [SerializeField]
    [Tooltip("The positional offset that this GameObject should have from the followed GameObject.")]
    Vector3 offset;

    private void Update()
    {
        transform.position = followThis.transform.position + offset;
    }
}