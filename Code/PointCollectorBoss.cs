using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointCollectorBoss : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    public GameObject door; 
    public GameObject UIWin; 
    public GameObject boss;
    public int score = 0; 

    private bool isBossDefeated = false; 

    void Start()
    {
        if (door != null)
        {
            door.SetActive(false);
        }

        if (UIWin != null)
        {
            UIWin.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Poin"))
        {
            Debug.Log("Player bersentuhan dengan objek Poin: " + other.gameObject.name);

            score++;

            UpdateScoreText();

            Destroy(other.gameObject);
            SFXManager.instance.PlayCollectItemSound();
        }

        if (other.CompareTag("Door") && isBossDefeated)
        {
            Debug.Log("Sukses! Player bersentuhan dengan pintu.");
            if (UIWin != null)
            {
                UIWin.SetActive(true);
            }

            Time.timeScale = 0;
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); 

            if (score == 5 && boss == null) 
            {
                Debug.Log("Poin sudah terkumpul dan bos sudah dikalahkan. Pintu terbuka!");

                if (door != null)
                {
                    door.SetActive(true);
                }
            }
        }
    }

    public void BossDefeated()
    {
        isBossDefeated = true; 
        Debug.Log("Bos telah dikalahkan!");

        if (score == 5 && door != null)
        {
            door.SetActive(true);
        }
    }
}
