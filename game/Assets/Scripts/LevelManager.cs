using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitingLoadingTime = 2f;
    public string nextLevel;

    //pausing and unpausing the game
    public bool isPaused;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    
    //waiting for next level?
    public IEnumerator LevelEnd()
    {
        // win music
        AudioManager.instance.PlayVictoryMusic();
        // stopping player from moving after finishing the level
        PlayerController.instance.canMove = false;
        // fading the screen out
        UIController.instance.StartFadeToBlack();
        // ????????
        yield return new WaitForSeconds(waitingLoadingTime);
        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }
}
