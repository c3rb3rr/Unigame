using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public float invicibleTime = 0.5f;
    // sfx
    public int playerPickupHealth;
    // which gun should be picked
    public Gun theGun;
    void Start()
    {
        
    }
    
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
            bool hasGun = false;
            if (theGun.name == PlayerController.instance.gunArm.GetChild(0).name)
            {
                hasGun = true;
            }

            if (!hasGun)
            {
                Gun gunClone = Instantiate(theGun);
                gunClone.transform.parent = PlayerController.instance.gunArm;
                gunClone.transform.position = PlayerController.instance.gunArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;
                PlayerController.instance.availableGuns.Add(gunClone);
                PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1;
                PlayerController.instance.SwitchGun();
            }
            
            Destroy(gameObject);
            AudioManager.instance.playSFX(playerPickupHealth);
        }
    }
}
