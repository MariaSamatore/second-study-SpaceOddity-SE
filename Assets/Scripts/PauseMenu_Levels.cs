using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PauseMenu_Levels : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject textUI;
    public GameObject progressBarUI;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

        //Hide UI stuff
        textUI.SetActive(false);
        progressBarUI.SetActive(false);


        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        //Hide UI stuff
        textUI.SetActive(false);
        progressBarUI.SetActive(false);
        
    }

    public void Resume()
    {
    
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

        //Show UI stuff
        textUI.SetActive(true);
        progressBarUI.SetActive(true);
        
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        //Show UI stuff
        textUI.SetActive(true);
        progressBarUI.SetActive(true);
        
    }
}
