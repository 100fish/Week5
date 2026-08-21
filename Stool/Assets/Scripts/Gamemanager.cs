using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    static ResidentPlayer player;

    public void Awake()
    {
        player = FindAnyObjectByType<ResidentPlayer>();
    }

    public static ResidentPlayer Get_Player() => player;
}


[System.Serializable]
public class Room
{
    public int room_id;
    public GameObject Room_Params;
}

