using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHP;
    public int maxHP;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        UIController.instance.healthSlider.maxValue = maxHP;
        UIController.instance.healthSlider.value = currentHP;
        UIController.instance.healthText.text = currentHP.ToString() + " / " + maxHP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer()
    {
        currentHP--;
        if (currentHP <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);
        }
        UIController.instance.healthSlider.value = currentHP;
        UIController.instance.healthText.text = currentHP.ToString() + " / " + maxHP.ToString();
    }
}
