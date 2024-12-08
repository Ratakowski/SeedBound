using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject UIPause;
    public GameObject UIOption;
    public GameObject BtnPause;
    public void Start()
    {
        Time.timeScale = 1;
        UIPause.SetActive(false);
        UIOption.SetActive(false);
    }
    public void TogglePause()
    {
        UIPause.SetActive(true);
        if (!isPaused)
        {
            BtnPause.SetActive(false);
            Time.timeScale = 0; 
            isPaused = true;
            Debug.Log("Game Paused");
        }
    }
    public void ToggleResume()
    {
        if(isPaused)
        {
            BtnPause.SetActive(true);
            UIPause.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
            Debug.Log("Game Resumed");
        }
    }
}
