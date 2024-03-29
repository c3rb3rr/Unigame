using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHP;
    public int maxHP;

    public float dmgInvicibleLength = 1f;
    private float _invincibleCount;
    
    // sfx
    public int playerDeath;
    public int playerHurt;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // powinno działać, ale nie działa XD
        // maxHP = CharacterTracker.instance.maxHealth;
        // currentHP = CharacterTracker.instance.currentHealth;
        currentHP = maxHP;
        UIController.instance.healthSlider.maxValue = maxHP;
        UIController.instance.healthSlider.value = currentHP;
        UIController.instance.healthText.text = currentHP.ToString() + " / " + maxHP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (_invincibleCount > 0)
        {
            _invincibleCount -= Time.deltaTime;
            if (_invincibleCount <= 0)
            {
                PlayerController.instance.bodySpriteRenderer.color = new Color(
                    PlayerController.instance.bodySpriteRenderer.color.r,
                    PlayerController.instance.bodySpriteRenderer.color.g,
                    PlayerController.instance.bodySpriteRenderer.color.b,
                    1f);
            }
        }
    }

    public void DamagePlayer()
    {
        if (_invincibleCount <= 0)
        {
            AudioManager.instance.playSFX(playerHurt);
            currentHP--;
            _invincibleCount = dmgInvicibleLength;
            
            PlayerController.instance.bodySpriteRenderer.color = new Color(
                PlayerController.instance.bodySpriteRenderer.color.r,
                PlayerController.instance.bodySpriteRenderer.color.g,
                PlayerController.instance.bodySpriteRenderer.color.b,
                .5f);
            
            if (currentHP <= 0)
            {
                AudioManager.instance.playSFX(playerDeath);
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();
            }

            UIController.instance.healthSlider.value = currentHP;
            UIController.instance.healthText.text = currentHP.ToString() + " / " + maxHP.ToString();
        }
    }

    public void MakeInvincible(float length)
    {
        _invincibleCount = length;
        PlayerController.instance.bodySpriteRenderer.color = new Color(
            PlayerController.instance.bodySpriteRenderer.color.r,
            PlayerController.instance.bodySpriteRenderer.color.g,
            PlayerController.instance.bodySpriteRenderer.color.b,
            .5f);
    }

    public void HealPlayer(int healthAmount)
    {
        if (currentHP < maxHP)
        {
            currentHP += healthAmount;
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
        }
        UIController.instance.healthSlider.value = currentHP;
        UIController.instance.healthText.text = currentHP.ToString() + " / " + maxHP.ToString();
    }
}
