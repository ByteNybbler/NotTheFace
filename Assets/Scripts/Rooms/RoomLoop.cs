// Author(s): Paul Calande
// Script that controls the room loop in Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoop : MonoBehaviour
{
    [System.Serializable]
    class RoomSpawnData
    {
        [Tooltip("The prefab to use to spawn the room.")]
        public GameObject prefab = null;
        [Tooltip("The width of the room in pixels.")]
        public float width = 0.0f;
    }

    [SerializeField]
    [Tooltip("How many rooms can exist in the loop before old rooms start getting destroyed.")]
    int maxConcurrentRooms;
    [SerializeField]
    [Tooltip("How many rooms are initially generated when the scene starts.")]
    int initialRoomCount;
    [SerializeField]
    RoomSpawnData roomBoss;
    [SerializeField]
    RoomSpawnData roomUpgrade;

    // The number of the latest room that has been generated.
    int roomNumber = 0;
    // The room number that the player should be in.
    int roomNumberPlayer = 0;
    // The order of rooms to spawn.
    RoomSpawnData[] roomOrder;
    // A collection of all the rooms that currently exist.
    Dictionary<int, Room> rooms = new Dictionary<int, Room>();

    private void Awake()
    {
        roomOrder = new RoomSpawnData[] { roomUpgrade, roomBoss };
    }

    private void Start()
    {
        for (int i = 0; i < initialRoomCount - 1; ++i)
        {
            CreateNextRoom();
        }
        // Automatically start the first room.
        rooms[0].StartRoom();
        // Set the room number of the player to 0 to start.
        roomNumberPlayer = 0;
    }

    private void MoveForwardByHalfRoomWidth(RoomSpawnData room)
    {
        transform.position += new Vector3(room.width * 0.01f * 0.5f, 0.0f, 0.0f);
    }

    private void CreateRoom(RoomSpawnData room)
    {
        if (roomNumber != 0)
        {
            RoomSpawnData previousRoom = roomOrder[(roomNumber - 1) % roomOrder.Length];
            MoveForwardByHalfRoomWidth(previousRoom);
        }
        MoveForwardByHalfRoomWidth(room);
        GameObject obj = Instantiate(room.prefab, transform.position, Quaternion.identity);
        Room rm = obj.GetComponent<Room>();
        rm.RoomStarted += Iterate;

        rooms.Add(roomNumber, rm);
        ++roomNumber;
    }

    private void DestroyRoom(Room room)
    {
        Destroy(room.gameObject);
    }

    private void CreateNextRoom()
    {
        RoomSpawnData roomToSpawn = roomOrder[roomNumber % roomOrder.Length];
        CreateRoom(roomToSpawn);
    }

    private void DestroyOldRoom()
    {
        int oldRoomNumber = roomNumber - maxConcurrentRooms - 1;
        Room room;
        if (rooms.TryGetValue(oldRoomNumber, out room))
        {
            DestroyRoom(room);
        }
    }

    public void Iterate()
    {
        CreateNextRoom();
        DestroyOldRoom();
        ++roomNumberPlayer;
    }
}