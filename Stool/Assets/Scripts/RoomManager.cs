using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public List<RoomMover> roomMovers = new List<RoomMover>();

    public static RoomManager instance;

    RoomMover current_roomMover;

    public void Awake()
    {
        instance = this;
    }

    int activate_new_player_position = -1;

    public void Activate_New_Room(int roomId, int roomStartID)
    {
        for (int i = 0; i < roomMovers.Count; i++)
        {
            if (roomMovers[i].Get_RoomID() != roomId) continue;

            if (current_roomMover != null) current_roomMover.Activate_Room(false);

            current_roomMover = roomMovers[i];
            current_roomMover.Activate_Room(true);

            activate_new_player_position = roomStartID;

            return;
        }
    }

    public void LateUpdate()
    {
        if(activate_new_player_position >= 0)
        {
            Set_Player(activate_new_player_position);
            activate_new_player_position = -1;
        }
    }

    public void Set_Player(int roomStartID)
    {
        List<Room_Start> _room_starts = current_roomMover.Get_RoomStarts();

        for (int i = 0; i < _room_starts.Count;i++)
        {
            if (_room_starts[i].RoomStartID != roomStartID) continue;
            print("OnLine : 3");
            Gamemanager.Get_Player().Get_CharacterController().enabled = false;
            Gamemanager.Get_Player().transform.position = _room_starts[i].Transform.position;
            Gamemanager.Get_Player().Get_CharacterController().enabled = true;
            print("OnLine : 4");
            //Gamemanager.Get_Player().Get_CharacterController().SimpleMove(_room_starts[i].Transform.localPosition);
        }
        
    }
}

[System.Serializable]
public class Room_Start
{
    public int RoomStartID;
    public Transform Transform;
}
