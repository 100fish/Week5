using UnityEngine;

public class RoomMoveCollider : MonoBehaviour
{
    RoomMover room;
    public int room_id_to_go_to;

    bool within = false;

    public void Awake()
    {
        room = transform.root.GetComponent<RoomMover>();

        MeshRenderer rend = transform.GetComponent<MeshRenderer>();
        rend.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;

        room.Set_New_Room(room_id_to_go_to);
    }
}
