using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 1;
    public float invicibleTime = 0.5f;
    // sfx
    public int playerPickupHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invicibleTime > 0)
        {
            invicibleTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && invicibleTime <= 0)
        {
            PlayerHealthController.instance.HealPlayer(healthAmount);
            Destroy(gameObject);
            AudioManager.instance.playSFX(playerPickupHealth);
        }
    }
}
