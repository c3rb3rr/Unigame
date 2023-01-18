using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider healthSlider;
    public Text healthText;
    public GameObject deathScreen;
    public Image betweenLevelScreen;
    public float fadeSpeed;
    private bool _fadeToBlack, _fadeOutBlack;
    
    // buttons
    public string newGameSceneButton, menuSceneButton;
    
    // pause menu
    public GameObject pauseMenu;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _fadeToBlack = false;
        _fadeOutBlack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeOutBlack)
        {
            betweenLevelScreen.color = new Color(betweenLevelScreen.color.r,
                betweenLevelScreen.color.g,
                betweenLevelScreen.color.b,
                Mathf.MoveTowards(betweenLevelScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (betweenLevelScreen.color.a == 0f)
            {
                _fadeOutBlack = false;
            }
        }
        
        if (_fadeToBlack)
        {
            betweenLevelScreen.color = new Color(betweenLevelScreen.color.r,
                betweenLevelScreen.color.g,
                betweenLevelScreen.color.b,
                Mathf.MoveTowards(betweenLevelScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (betweenLevelScreen.color.a == 1f)
            {
                _fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        _fadeToBlack = true;
        _fadeOutBlack = false;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneButton);
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameSceneButton);
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
}
