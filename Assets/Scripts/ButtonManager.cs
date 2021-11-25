using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    
    public GameObject menu;
    public GameObject tutorial;
    public GameObject menuUI;
    public GameObject tutorialUI;
    public GameObject tutorialImage;
    public GameObject resumeButton;
    public GameObject credits;
    public SceneTransition sceneTransition;

    private bool gameStarted;

    void Start()
    {
        //Aimer.chewValue = 0;
        gameStarted = false;
    }

    public void MainMenu()
    {
        menuUI.SetActive(true);
        credits.SetActive(false);
    }

    public void Tutorial()
    {
        if (!gameStarted)
        {
            menu.SetActive(false);
            menuUI.SetActive(false);
            credits.SetActive(false);
            tutorial.SetActive(true);
            tutorialUI.SetActive(true);
        }
    }

    public void Back()
    {
        MainMenu();
    }


    public void StartGame()
    {
        if (LevelManager.manager.levelIndex < 1)
        {
            LevelManager.manager.levelIndex++;
            sceneTransition.StartCoroutine("LoadLevel");
        }
        //Aimer.chewValue = 0;
        gameStarted = true;
        
        //SceneManager.LoadScene(LevelManager.manager.levelIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        LevelManager.manager.levelIndex = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelManager.manager.levelIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        resumeButton.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        resumeButton.SetActive(false);
    }

    public void Credits()
    {
        if (!gameStarted)
        {
            menuUI.SetActive(false);
            credits.SetActive(true);
        }
    }
    
}
