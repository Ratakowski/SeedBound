using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string cutsceneName = "CutScene 2";
    public GameObject continueButton;

    private void Start()
    {
        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }
    }
    public void OnLevelComplete()
    {
        if (continueButton != null)
        {
            continueButton.SetActive(true);
        }
    }
    public void GoToCutscene()
    {
        if (!string.IsNullOrEmpty(cutsceneName))
        {
            SceneManager.LoadScene(cutsceneName);
        }
        else
        {
            Debug.LogError("Cutscene name not assigned!");
        }
    }
}