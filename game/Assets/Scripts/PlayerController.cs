using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 _moveInput;
    // where is player position - used in EnemyController script
    public static PlayerController instance;
    public Rigidbody2D playerRigidbody2D;
    public Transform gunArm;
    private Camera _cam;
    public Animator anim;
    // reference to the bullet that we want to fire
    public GameObject bullet;
    //from where we are fireing the bullet (position on the world)
    public Transform fireStartPoint;
    // Start is called before the first frame update
    public float fireOfRate;
    private float _bulletCounter;
    // used for changing the alfa channel while invincible in playerHealthController
    public SpriteRenderer bodySpriteRenderer;

    private float _activeMoveSpeed;
    public float dashSpeed = 8f;
    public float dashLength = .5f;
    public float dashCooldown = 3f;
    public float dashInvincibility = .5f;
    private float _dashCoolCounter;
    [HideInInspector]
    public float dashCounter;
    // sfx
    public int playerDash;
    public int playerShooting;
    
    [HideInInspector]
    // player shouldnt move after completing the level
    public bool canMove = true; // at start player can move

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _cam = Camera.main;
        _activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused)
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
            _moveInput.Normalize();

            // transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed,
            //     moveInput.y * Time.deltaTime * moveSpeed, 0f);
            playerRigidbody2D.velocity = _moveInput * _activeMoveSpeed;
            // position of mouse
            Vector3 mousePos = Input.mousePosition;
            // position of main camera
            Vector3 screenPoint = _cam.WorldToScreenPoint(transform.localPosition);

            //rotating player
            if (mousePos.x < screenPoint.x) // to the left of the player
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                gunArm.localScale = new Vector3(1f, 1f, 1f);
            }

            // ratate gun
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            // angle that gun should point to
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(0, 0, angle);

            if (Input.GetMouseButtonDown(0)) // if lpm  is clicked
            {
                // create a copy of specific object
                Instantiate(bullet, fireStartPoint.position, fireStartPoint.rotation);
                _bulletCounter = fireOfRate;
                RandomShootingSfx();
            }

            if (Input.GetMouseButton(0))
            {
                _bulletCounter -= Time.deltaTime;
                if (_bulletCounter <= 0)
                {
                    Instantiate(bullet, fireStartPoint.position, fireStartPoint.rotation);
                    _bulletCounter = fireOfRate;
                    RandomShootingSfx();
                }
            }

            // mechanic of dashing
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (_dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    _activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;

                    anim.SetTrigger("dash");
                    PlayerHealthController.instance.MakeInvincible(dashInvincibility);
                    AudioManager.instance.playSFX(playerDash);
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    _activeMoveSpeed = moveSpeed;
                    _dashCoolCounter = dashCooldown;
                }
            }

            if (_dashCoolCounter > 0)
            {
                _dashCoolCounter -= Time.deltaTime;
            }

            // animation of walking
            if (_moveInput != Vector2.zero)
            {
                anim.SetBool("isPlayerMoving", true);
            }
            else
            {
                anim.SetBool("isPlayerMoving", false);
            }
        }
        else
        {
            // stop moving
            playerRigidbody2D.velocity = Vector2.zero;
            // stop animation
            anim.SetBool("isPlayerMoving", false);
        }
    }

    public void RandomShootingSfx()
    {
        playerShooting = Random.Range(11, 16);
        AudioManager.instance.playSFX(playerShooting);
    }
}
