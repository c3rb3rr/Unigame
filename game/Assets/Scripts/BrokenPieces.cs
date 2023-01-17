using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrokenPieces : MonoBehaviour
{
    public float moveSpeed = 5;
    private Vector3 _moveDirection;
    public float deceleration = 3;
    public float lifeTime = 10;
    public SpriteRenderer pieceSpriteRenderer;
    public float fadeSpeed = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        _moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        _moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _moveDirection * Time.deltaTime;
        _moveDirection = Vector3.Lerp(_moveDirection, Vector3.zero, deceleration * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            pieceSpriteRenderer.color = new Color(pieceSpriteRenderer.color.r,
                pieceSpriteRenderer.color.g,
                pieceSpriteRenderer.color.b,
                Mathf.MoveTowards(pieceSpriteRenderer.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (pieceSpriteRenderer.color.a == 0f)
                Destroy(gameObject);
        }
    }
}
