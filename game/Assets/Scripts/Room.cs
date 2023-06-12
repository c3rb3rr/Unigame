using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeDoorsWhenEntered;
    // public bool openRoomWhenEnemiesCleared;
    public GameObject[] doors;
    [HideInInspector]
    public bool roomActive;
    // public List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (enemies.Count > 0 && roomActive && openRoomWhenEnemiesCleared)
        // {
        //     for (int i = 0; i < enemies.Count; i++)
        //     {
        //         if (enemies[i] == null)
        //         {
        //             enemies.RemoveAt(i);
        //             i--;
        //         }
        //     }
        //
        //     if (enemies.Count == 0)
        //     {
        //         foreach (GameObject door in doors)
        //         {
        //             door.SetActive(false);
        //             closeDoorsWhenEntered = false;
        //         }
        //     }
        // }
    }

    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
            closeDoorsWhenEntered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
            if (closeDoorsWhenEntered)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }

            roomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            roomActive = false;
        }
    }
}
