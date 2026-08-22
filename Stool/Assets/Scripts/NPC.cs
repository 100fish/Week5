using UnityEngine;

public class NPC : Interactable
{
    public string Name;
    public RoomMover TalkRoom;

    public override void Interact()
    {
        Talk(Name, Get_Current_DialougeText(), TalkRoom);
    }
}