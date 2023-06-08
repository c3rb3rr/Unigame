using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnBossScript : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D BoxCollider2D;
    // This is stupid way of doing this ideally it shouldn't be sprite but UI-something but I can't be bothered...
    [SerializeField]
    private SpriteRenderer BossCardRenderer;
    [SerializeField]
    private GameObject Boss;
    
    
    // Start is called before the first frame update
    void Start()
    {
        var color = BossCardRenderer.color;
        BossCardRenderer.color = new Color(color.r, color.g, color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Disable player movement
            PlayerController.instance.canMove = false;

            //move player a little bit to the middle of the room
            var playerPosition = PlayerController.instance.transform.position;
            var center = BoxCollider2D.transform.position;
            
            //hideUI
            UIController.instance.gameObject.SetActive(false);
            
            PlayerController.instance.gameObject.transform.position = Vector3.Lerp(playerPosition, center, 0.25f);;
            StartCoroutine(ChangeAlphaChannelToValueOverTiem(1, 0.25f));
            BoxCollider2D.enabled = false;
            AudioManager.instance.playSFX(20);
            StartCoroutine(FadeBossCardAfterTime());
        }
    }
    
    IEnumerator FadeBossCardAfterTime()
    {
        //Wait for 2 seconds
        yield return new WaitForSeconds(2);
        StartCoroutine(ChangeAlphaChannelToValueOverTiem(0, 0.25f));
        Boss.SetActive(true);
        //Enable player movement
        PlayerController.instance.canMove = true;
        //unUI
        UIController.instance.gameObject.SetActive(true);

    }
    
    private IEnumerator ChangeAlphaChannelToValueOverTiem(float alpha, float duration)
    {
        var startColor = BossCardRenderer.color;
        var endColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
        float time = 0;
        while (time < duration) {
            time += Time.deltaTime;
            BossCardRenderer.color = Color.Lerp(startColor, endColor, time/duration);
            yield return null;
        }
    }



}
