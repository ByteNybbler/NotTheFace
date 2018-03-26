using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStartTrigger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the Room that owns this trigger.")]
    Room room;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (room.GetStatus() == Room.Status.Waiting)
        {
            if (collision.CompareTag("Player"))
            {
                room.StartRoom();
            }
        }
    }
}