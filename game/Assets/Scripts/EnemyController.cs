using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Basics")]
    public SpriteRenderer body;
    public Rigidbody2D rb2d;
    public float moveSpeed;
    public Vector3 moveDirection;
    public Animator anim;
    public int heathPoints = 200;
    public GameObject[] deathSplatters;
    public GameObject hitEffect;
    
    [Header("Chasing Player")]
    public float whenToChasePlayer;
    public bool shouldChasePlayer;

    [Header("Bullets")]
    public bool shouldShoot;
    public GameObject bullet;
    public Transform fireStartPoint;
    public float fireOfRate;
    public float bulletCounter;
    public float shootRange;

    // sfx
    public int enemyDeath;
    public int enemyHurt;


    // new enemies - running away
    // [Header("Running Away from Player")]
    // public bool shouldRunAway;
    // public float runAwayRange;

    // new enemy - wandering
    [Header("Wandering")]
    public bool shouldWander;
    public float wanderTime;
    public float wanderPauseLength;
    private float _wanderCounter, _pauseCounter;
    private Vector3 _wanderDirection;
    // private float _randomWanderCounterTime;
    
    // enemy - patroling
    [Header("Patroling")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int _currentPatrolPoint;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (shouldShoot)
        {
            _pauseCounter = Random.Range(wanderPauseLength * 0.1f, wanderPauseLength * 1.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (body.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            moveDirection = Vector3.zero;
            
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < whenToChasePlayer && shouldChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                if (shouldWander)
                {
                    if (_wanderCounter > 0)
                    {
                        _wanderCounter -= Time.deltaTime;
                        // _randomWanderCounterTime = Random.Range(0, _wanderCounter);
                        // TODO: dodać więcej random ruchów, że rusza się przez losowy czas, a nie zawsze tak samo długo
                        moveDirection = _wanderDirection;
                        
                        if (_wanderCounter <= 0)
                        {
                            _pauseCounter = Random.Range(wanderPauseLength * 0.1f, wanderPauseLength * 1.2f);
                        }
                    }

                    if (_pauseCounter > 0)
                     {
                         _pauseCounter -= Time.deltaTime;

                         if (_pauseCounter <= 0)
                         {
                             _wanderCounter = Random.Range(wanderPauseLength * 0.1f, wanderPauseLength * 1.2f);
                             _wanderDirection = new Vector3(Random.Range(-1, 1f), Random.Range(-1f, 1f), 0f);
                         }
                     }
                }

                if (shouldPatrol)
                {
                    moveDirection = patrolPoints[_currentPatrolPoint].position - transform.position;
                    if (Vector3.Distance(transform.position, patrolPoints[_currentPatrolPoint].position) < .5f)
                    {
                        _currentPatrolPoint++;
                        if (_currentPatrolPoint >= patrolPoints.Length)
                        {
                            _currentPatrolPoint = 0;
                        }
                    }
                }
            }

            // if (shouldRunAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) <
            //     runAwayRange)
            // {
            //     moveDirection = -1 * (PlayerController.instance.transform.position - transform.position);
            // }
            
            
            // else
            // {
            //     moveDirection = Vector3.zero;
            // }

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

    public void DamageEnemy(int damage)
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        heathPoints -= damage;
        AudioManager.instance.playSFX(enemyHurt);
        if (heathPoints <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.playSFX(enemyDeath);
            Instantiate(deathSplatters[Random.Range(0, deathSplatters.Length)], transform.position,
                Quaternion.Euler(0, 0, Random.Range(0, 359)));
        }
    }
}
