using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitingLoadingTime = 2f;
    public string nextLevel;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
