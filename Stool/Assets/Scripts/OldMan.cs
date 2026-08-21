using UnityEngine;

public class OldMan : Interactable
{
    public string Name;

    public override void Interact()
    {
        Talk(Name, Get_Current_DialougeText());
    }
}
