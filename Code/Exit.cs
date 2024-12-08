using UnityEngine;

public class Exit : MonoBehaviour
{
    public void QuitApp()
    {
        Debug.Log("Aplikasi akan keluar.");
        Application.Quit();
    }
}
