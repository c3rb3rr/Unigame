using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public static CharacterSelectManager instance;
    public PlayerController activePlayer;
    public CharacterSelector activeCharSelect;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void Awake()
    {
        instance = this;
    }
}
