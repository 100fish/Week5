using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class RoomMover : MonoBehaviour
{
    public bool start_room = false;
    public Room room;
    public RoomMover[] rooms;

    private void Awake()
    {
        //room.StartPosition

        if (start_room) return;
        room.Room_Params.SetActive(false);
    }

    public void Set_New_Room(int room_id)
    {
        for(var i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].room.room_id != room_id) continue;

            rooms[i].room.Room_Params.SetActive(true);
            room.Room_Params.SetActive(false);

            //Gamemanager.Get_Player().transform.position = room.StartPosition.position;

            break;
        }
    }
}