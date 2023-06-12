using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    public static CharacterTracker instance;

    public int currentHealth, maxHealth;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
