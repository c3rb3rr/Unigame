using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAwayEnemyController : EnemyController
{
    [Header("Running Away from Player")]
    public bool shouldRunAway;
    public float runAwayRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (body.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (shouldRunAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) <
                runAwayRange)
            {
                moveDirection = -1 * (PlayerController.instance.transform.position - transform.position);
            }
        
            
            moveDirection.Normalize();
            rb2d.velocity = moveDirection * moveSpeed;

            // animation of walking
            if (moveDirection != Vector3.zero)
            {
                anim.SetBool("isEnemyMoving", true);
            }
            else
            {
                anim.SetBool("isEnemyMoving", false);
            }

            if (shouldShoot & Vector3.Distance(PlayerController.instance.transform.position, transform.position) < shootRange)
            {
                bulletCounter -= Time.deltaTime;
                if (bulletCounter <= 0)
                {
                    Instantiate(bullet, fireStartPoint.position, fireStartPoint.rotation);
                    PlayerController.instance.RandomShootingSfx();
                    bulletCounter = fireOfRate;
                }
            }
        }
        else
        {
            rb2d.velocity = Vector2.zero; //when player is dead, stop moving 
        }            
    }
}
