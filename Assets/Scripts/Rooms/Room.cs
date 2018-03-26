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
    RoomDoor doorEntry;
    [SerializeField]
    RoomDoor doorExit;

    public Status GetStatus()
    {
        return status;
    }

    public void LockEntrance()
    {
        doorEntry.Close();
    }

    public void OpenExit()
    {
        doorExit.Open();
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