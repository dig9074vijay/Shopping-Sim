using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject _pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Espace pressed");
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                pauseGame();
            }
        }
        
    }

    public void pauseGame()
    {
        Debug.Log("pauseGamecalled");
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;

    }

    public void Resume()
    {
        Debug.Log("ResumeGamecalled");

        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;

    }

    public void openMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
