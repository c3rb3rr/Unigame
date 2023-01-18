using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Vector3 _direction;
    // sfx
    public int bulletImpact = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        _direction = PlayerController.instance.transform.position - transform.position;
        _direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
        Destroy(gameObject);
        AudioManager.instance.playSFX(bulletImpact);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
