using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMati : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Objek yang masuk trigger: {other.gameObject.name}");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player jatuh!");
            PlayerHP nyawa = other.GetComponent<PlayerHP>();
            if (nyawa != null)
            {
                nyawa.TerkenaSerangan(nyawa.NyawaTersisa);
                Debug.Log("Nyawa Player habis!");
            }
            else
            {
                Debug.LogWarning("Komponen Nyawa tidak ditemukan pada Player!");
            }
        }
    }
}
