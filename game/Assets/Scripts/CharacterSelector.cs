using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private bool _canSelect;
    public GameObject message;
    public PlayerController playerToSpawn;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (_canSelect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 playerPos = PlayerController.instance.transform.position;
                Destroy(PlayerController.instance.gameObject);
                PlayerController newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);
                PlayerController.instance = newPlayer;
                gameObject.SetActive(false);
                CameraController.instance.target = newPlayer.transform;
                CharacterSelectManager.instance.activePlayer = newPlayer;
                CharacterSelectManager.instance.activeCharSelect.gameObject.SetActive(true);
                CharacterSelectManager.instance.activeCharSelect = this;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _canSelect = true;
            message.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _canSelect = false;
            message.SetActive(false);
        }
    }
}
