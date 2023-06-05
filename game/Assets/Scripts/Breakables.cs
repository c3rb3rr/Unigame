using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Breakables : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    // if random number is equal or higher than this, box drop random item
    public float itemDropPercent;
    public int maxPieces = 5;
    // sfx
    public int breakSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash()
    {
        Destroy(gameObject);
        AudioManager.instance.playSFX(breakSound);
        //showing broken pieces after destroying the box
        int piecesToDrop = Random.Range(1, maxPieces);
        for (int i=0; i<piecesToDrop; i++)
        { 
            Instantiate(brokenPieces[Random.Range(0, brokenPieces.Length)], transform.position,
                Quaternion.Euler(0, 0, Random.Range(0, 359)));
        }
        //droping items
        if (shouldDropItem)
        {
            float dropChance = Random.Range(0f, 100f);
            if (dropChance < itemDropPercent)
            {
                int randomItem = Random.Range(0, itemsToDrop.Length);
                Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerController.instance.dashCounter > 0)
            {
                Smash();
            }
        }

        if (other.tag == "PlayerBullet")
        {
            Smash();
        }
    }
}
