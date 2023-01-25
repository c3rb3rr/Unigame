using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFloor : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public bool openRoomWhenEnemiesCleared;
    [HideInInspector]
    public Room theRoom;
    
    // Start is called before the first frame update
    void Start()
    {
        if (openRoomWhenEnemiesCleared)
        {
            theRoom.closeDoorsWhenEntered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && theRoom.roomActive && openRoomWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                theRoom.OpenDoors();
            }
        }
    }
}
