// Author(s): Paul Calande
// A door of a room.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the door GameObject.")]
    GameObject door;
    [SerializeField]
    [Tooltip("Whether the door is currently open.")]
    bool isOpen;

    /*
    private void Awake()
    {
        door.SetActive(!isOpen);
    }
    */

    public void Open()
    {
        door.SetActive(false);
    }

    public void Close()
    {
        door.SetActive(true);
    }
}