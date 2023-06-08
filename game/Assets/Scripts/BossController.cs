using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public SpriteRenderer body;
    public Rigidbody2D rb2d;
    public BoxCollider2D swordHitbox;
    public float moveSpeed;
    public bool enemyActive;
    private Vector3 _moveDirection;
    public Animator anim;
    public int heathPoints;
    public bool isAttacking;
    public AudioManager audioManager;
    public bool isWaiting = true;
    public RoomFloor RoomFloor;
    public GameObject LevelExit;
    public float shootRange;

    public int hitCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GetComponent<AudioManager>();
        RoomFloor = GetComponent<RoomFloor>();
        //switch to combat after 3s
        StartCoroutine(WaitForCombat());
    }

    // Update is called once per frame
    void Update()
    {
        if (body.isVisible && PlayerController.instance.gameObject.activeInHierarchy && heathPoints > 0 && !isWaiting)
        {


            //Move towards player
            if (enemyActive && !isAttacking)
            {
                var playerPosition = PlayerController.instance.transform.position;
                var bossPosition = transform.position;
                var offset = new Vector3(playerPosition.x < bossPosition.x ? 1.0f : -1.0f, -2.5f);
                _moveDirection = playerPosition - bossPosition + offset;
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
                //Run
                body.transform.localScale = new Vector3(_moveDirection.x >=0 ? -2.5f : 2.5f, 2.5f, 1.0f);

                anim.SetInteger("AnimState", 2);
            }
            else
            {
                anim.SetInteger("AnimState", 1);
            }

            //attack
            if (!isAttacking && Vector3.Distance(PlayerController.instance.transform.position, transform.position) <
                shootRange)
            {
                // audioManager.playSFX(18);
                _moveDirection = Vector3.zero;
                anim.SetTrigger("Attack");
            }
        }
        else
        {
            rb2d.velocity = Vector2.zero; //when player is dead, stop moving 
        }
    }

    private IEnumerator WaitForCombat()
    {
        anim.SetInteger("AnimState", 0);
        yield return new WaitForSeconds(2);
        anim.SetInteger("AnimState", 1);
        yield return new WaitForSeconds(1);

        isWaiting = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (swordHitbox.isActiveAndEnabled && other.CompareTag("Player"))
        {
            audioManager.playSFX(18);
            PlayerHealthController.instance.DamagePlayer();
        }
    }


    public void DamageEnemy(int damage)
    {
        hitCounter++;
        if (hitCounter % 10 == 0)
        {
            audioManager.playSFX(19);
            anim.SetTrigger("Hurt");
        }

        heathPoints -= damage;

        if (heathPoints <= 0)
        {
            StartCoroutine(FadeAlphaToZero( 4f));
            rb2d.simulated = false;
            anim.SetTrigger("Death");
            UnlockDoors();
        }
    }

    private void UnlockDoors()
    {
        LevelExit.SetActive(true);
        RoomFloor.theRoom.OpenDoors();
    }

    private IEnumerator FadeAlphaToZero(float duration)
    {
        var startColor = body.color;
        var endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        float time = 0;
        while (time < duration) {
            time += Time.deltaTime;
            body.color = Color.Lerp(startColor, endColor, time/duration);
            yield return null;
        }
    }

}