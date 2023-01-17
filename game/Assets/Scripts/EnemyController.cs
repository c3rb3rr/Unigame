using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SpriteRenderer body;
    public Rigidbody2D rb2d;
    public float moveSpeed;
    public float whenToChasePlayer;
    private Vector3 _moveDirection;
    public Animator anim;
    public int heathPoints = 200;
    public GameObject[] deathSplatters;
    public GameObject hitEffect;

    public bool shouldShoot; 
    public GameObject bullet;
    public Transform fireStartPoint;
    public float fireOfRate;
    private float _bulletCounter;
    public float shootRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (body.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < whenToChasePlayer)
            {
                _moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                _moveDirection = Vector3.zero;
            }

            _moveDirection.Normalize();
            rb2d.velocity = _moveDirection * moveSpeed;

            // animation of walking
            if (_moveDirection != Vector3.zero)
            {
                anim.SetBool("isEnemyMoving", true);
            }
            else
            {
                anim.SetBool("isEnemyMoving", false);
            }

            if (shouldShoot & Vector3.Distance(PlayerController.instance.transform.position, transform.position) < shootRange)
            {
                _bulletCounter -= Time.deltaTime;
                if (_bulletCounter <= 0)
                {
                    Instantiate(bullet, fireStartPoint.position, fireStartPoint.rotation);
                    _bulletCounter = fireOfRate;
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
        if (heathPoints <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathSplatters[Random.Range(0, deathSplatters.Length)], transform.position,
                Quaternion.Euler(0, 0, Random.Range(0, 359)));
        }
    }
}
