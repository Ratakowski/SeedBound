using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Option()
    {
        SceneManager.LoadScene("Option");
    }

    public void LevelMenu()
    {
        SceneManager.LoadScene("Level Menu");
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void Bos()
    {
        SceneManager.LoadScene("Boss Level");
    }

    public void CutScene1()
    {
        SceneManager.LoadScene("CutScene 1");
    }

    public void CutScene2()
    {
        SceneManager.LoadScene("CutScene 2");
    }

    public void CutScene3()
    {
        SceneManager.LoadScene("CutScene 3");
    }

    public void CutScene4()
    {
        SceneManager.LoadScene("CutScene 4");
    }
    public void CutSceneEnding()
    {
        SceneManager.LoadScene("CutScene Ending");
    }
    public void CutSceneEpilog()
    {
        SceneManager.LoadScene("CutScene Epilog");
    }
}
