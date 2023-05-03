using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStaminaController : MonoBehaviour
{
    [SerializeField]
    public Slider StaminaBar;
    
    private int MaxStamina { get; set; }
    private int CurrentStamina { get; set; }
    // Start is called before the first frame update
    public static PlayerStaminaController instance;
    private WaitForSeconds regenTick = new(0.1f);
    private Coroutine regen;
    void Start()
    {
        MaxStamina = 100;
        CurrentStamina = 100;
        StaminaBar.value = CurrentStamina;
    }

    private void Awake()
    {
        instance = this;
    }

    public bool UseStamina(int amount)
    {
        if (CurrentStamina - amount >= 0)
        {
            CurrentStamina -= amount;
            StaminaBar.value = CurrentStamina;
            if (regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(RegenerateStamina());
            return true;
        }

        return false;
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(2);

        while (CurrentStamina < MaxStamina)
        {
            CurrentStamina += MaxStamina / 100;
            StaminaBar.value = CurrentStamina;
            yield return regenTick;
        }

        regen = null;
    }
}
