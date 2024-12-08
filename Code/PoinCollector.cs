using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoinCollector : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    public GameObject door;
    public GameObject UIWin;
    public int score = 0;

    void Start()
    {
        if (door != null)
        {
            door.SetActive(false);
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

        if (other.CompareTag("Door"))
        {
            Debug.Log("Sukses! Player bersentuhan dengan pintu.");
            UIWin.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); 

            if (score == 5)
            {
                Debug.Log("Poin sudah terkumpul " + score + ", saatnya pulang.");

                if (door != null)
                {
                    door.SetActive(true);
                }
            }
        }
    }
}
