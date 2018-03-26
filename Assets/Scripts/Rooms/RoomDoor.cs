// Author(s): Paul Calande
// A door of a room.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Whether the door is currently open.")]
    bool isOpen;

    private void Awake()
    {
        gameObject.SetActive(!isOpen);
    }

    public void Open()
    {
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(true);
    }
}