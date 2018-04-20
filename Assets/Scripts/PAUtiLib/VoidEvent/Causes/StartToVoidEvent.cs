// Author(s): Paul Calande
// Invokes a VoidEvent when this GameObject is started.

using UnityEngine;

public class StartToVoidEvent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to fire.")]
    VoidEvent voidEvent;

    private void Start()
    {
        voidEvent.Fire();
    }
}