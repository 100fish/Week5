using UnityEngine;

public class RoomMoveCollider : MonoBehaviour
{
    [SerializeField] private int RoomID = 0;
    [SerializeField] private int RoomStartID = 0;

    public void OnTriggerEnter(Collider other)
    {
        print("OnLine : 1");
        if(other.gameObject.tag == "Player")
        {
            print("OnLine : 2");
            RoomManager.instance.Activate_New_Room(RoomID, RoomStartID);
        }
       
    }
}
