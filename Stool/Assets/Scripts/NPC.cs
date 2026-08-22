using UnityEngine;

public class NPC : Interactable
{
    public string Name;

    public override void Interact()
    {
        Talk(Name, Get_Current_DialougeText());
    }
}