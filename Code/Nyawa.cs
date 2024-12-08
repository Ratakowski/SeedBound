using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nyawa : MonoBehaviour
{
    [SerializeField] private float NyawaAwal = 5f; 
    public float NyawaTersisa;                     
    private Animator anim;
    private bool Mati;

    private MusuhPatroli musuhPatroli;  

    private void Awake()
    {
        NyawaTersisa = NyawaAwal;
        Debug.Log($"{gameObject.name} initial health: {NyawaTersisa}");
        anim = GetComponent<Animator>();
        musuhPatroli = GetComponent<MusuhPatroli>(); 
    }

    public void TerkenaSerangan(float _damage)
    {
        NyawaTersisa = Mathf.Clamp(NyawaTersisa - _damage, 0, NyawaAwal);
        Debug.Log($"{gameObject.name} terkena serangan! Sisa nyawa: {NyawaTersisa}");

        if (NyawaTersisa > 0)
        {
            if (anim != null) anim.SetTrigger("Sakit");
        }
        else
        {
            if (!Mati)
            {
                Mati = true;
                if (anim != null) anim.SetTrigger("Mati");
                Debug.Log($"{gameObject.name} has died.");
                if (musuhPatroli != null)
                {
                    musuhPatroli.enabled = false; 
                }
                StartCoroutine(HilangSetelahMati());
            }
        }
    }

    private IEnumerator HilangSetelahMati()
    {
        yield return new WaitForSeconds(5f); 
        if (gameObject.name == "Geoki")
        {
            Debug.Log($"{gameObject.name} tidak dihancurkan karena namanya adalah 'Geoki'.");
        }
        else
        {
            Debug.Log($"{gameObject.name} has disappeared.");
            Destroy(gameObject);
        }
    }
}
