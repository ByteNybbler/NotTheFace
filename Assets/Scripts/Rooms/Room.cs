// Author(s): Paul Calande
// Room script for Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public delegate void RoomStartedHandler();
    public event RoomStartedHandler RoomStarted;
    public delegate void RoomFinishedHandler();
    public event RoomFinishedHandler RoomFinished;

    public enum Status
    {
        Waiting,
        Active,
        Finished
    }
    Status status = Status.Waiting;

    [SerializeField]
    [Tooltip("Reference to the entrance of the room.")]
    RoomDoor doorEntry;
    [SerializeField]
    [Tooltip("Reference to the exit of the room.")]
    RoomDoor doorExit;
    [SerializeField]
    [Tooltip("Reference to the item pool.")]
    ItemPool itemPool;
    [SerializeField]
    [Tooltip("Reference to the boss pool.")]
    BossPool bossPool;
    [SerializeField]
    [Tooltip("Reference to the player health meter, if one exists in this room.")]
    HealthToMeter playerHealth;
    [SerializeField]
    [Tooltip("The bottom left edge of the room.")]
    Transform bottomLeft;
    [SerializeField]
    [Tooltip("The bottom right edge of the room.")]
    Transform bottomRight;

    // How many times the RoomLoop looped through the full cycle of rooms by the time
    // this room was generated.
    int loopNumber;

    public Status GetStatus()
    {
        return status;
    }

    public void LockEntrance()
    {
        if (doorEntry != null)
        {
            doorEntry.Close();
        }
    }

    public void OpenExit()
    {
        if (doorExit != null)
        {
            doorExit.Open();
        }
    }

    public void StartRoom()
    {
        status = Status.Active;
        LockEntrance();
        OnRoomStarted();
    }

    public void FinishRoom()
    {
        status = Status.Finished;
        OpenExit();
        OnRoomFinished();
    }

    public void SetItemPool(ItemPool val)
    {
        itemPool = val;
    }

    public ItemPool GetItemPool()
    {
        return itemPool;
    }

    public void SetBossPool(BossPool val)
    {
        bossPool = val;
    }

    public BossPool GetBossPool()
    {
        return bossPool;
    }

    public void SetDoorEntry(RoomDoor door)
    {
        doorEntry = door;
    }

    public void SetDoorExit(RoomDoor door)
    {
        doorExit = door;
    }

    public int GetLoopNumber()
    {
        return loopNumber;
    }

    public void SetLoopNumber(int val)
    {
        loopNumber = val;
    }

    public void SetPlayerHealth(Health health)
    {
        if (playerHealth != null)
        {
            playerHealth.SetHealth(health);
        }
    }

    public Vector3 GetRandomFloorPosition()
    {
        return
            UtilRandom.PositionBetweenTwoPoints(bottomLeft.position, bottomRight.position);
    }

    private void OnRoomStarted()
    {
        if (RoomStarted != null)
        {
            RoomStarted();
        }
    }

    private void OnRoomFinished()
    {
        if (RoomFinished != null)
        {
            RoomFinished();
        }
    }
}