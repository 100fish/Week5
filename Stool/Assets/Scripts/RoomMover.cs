using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomMover : MonoBehaviour
{
    [SerializeField] private int RoomID = 0;

    [SerializeField] private List<Room_Start> room_starts;

    [SerializeField] private bool Start_Room = false;

    public int Get_RoomID() => RoomID;

    public List<Room_Start> Get_RoomStarts() => room_starts;

    public void Awake()
    {
        if (!Start_Room) Activate_Room(false);
        else RoomManager.instance.Set_Current_Room(this);

            for (var i = 0; i < room_starts.Count; i++)
            {
                room_starts[i].Transform.gameObject.SetActive(false);
            }
    }

    public void Activate_Room(bool activate)
    {
        gameObject.SetActive(activate);
    }
}