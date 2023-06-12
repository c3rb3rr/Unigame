using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    //from where we are fireing the bullet (position on the world)
    public Transform fireStartPoint;
    // Start is called before the first frame update
    public float fireOfRate;
    private float _bulletCounter;
    private string _prefabName;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            if (_bulletCounter > 0)
            {
                _bulletCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) // if lpm  is clicked
                {
                    if (gameObject.name == "Shotgun" || gameObject.name == "Shotgun(Clone)")
                    {
                        float initialAngle = fireStartPoint.rotation.eulerAngles.z;
                        for (int i = 0; i < 3; i++)
                        {
                            float bulletAngle = initialAngle + (i - 1) * 10f;
                            Instantiate(bullet, fireStartPoint.position, Quaternion.Euler(0f, 0f, bulletAngle));
                        }
                    }
                    else
                    { 
                        Instantiate(bullet, fireStartPoint.position, fireStartPoint.rotation);   
                    }
                    
                    _bulletCounter = fireOfRate;
                    PlayerController.instance.RandomShootingSfx();
                }
            }
        }
    }
}
