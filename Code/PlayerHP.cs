using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] public float NyawaAwal = 10f;
    public float NyawaTersisa;
    public Image HPField;
    private Animator anim;
    private bool Mati;
    public GameObject UIGameOver;
    void Start()
    {
        NyawaTersisa = NyawaAwal;
        UIGameOver.SetActive(false);
    }
    private void Awake()
    {
        Debug.Log($"{gameObject.name} initial health: {NyawaTersisa}");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float refHealth= 0;
        refHealth = NyawaTersisa / NyawaAwal;
        HPField.fillAmount = refHealth;
    }

    public void TerkenaSerangan(float _damage)
    {
        NyawaTersisa = Mathf.Clamp(NyawaTersisa - _damage, 0, NyawaAwal);
        Debug.Log($"{gameObject.name} terkena serangan! Sisa nyawa: {NyawaTersisa}");

        if (NyawaTersisa > 0)
        {
            if (anim != null) anim.SetTrigger("Sakit");
            SFXManager.instance.PlayDamageTakenSound();
        }
        else
        {
            if (!Mati)
            {
                Mati = true;
                if (anim != null) anim.SetTrigger("Mati");
                SFXManager.instance.PlayDamageTakenSound();
                Debug.Log($"{gameObject.name} has died.");
                var characterMovement = GetComponent<CharacterMovement>();
                if (characterMovement != null) characterMovement.enabled = false;
                StartCoroutine(HilangSetelahMati());
            }
        }
    }

    private IEnumerator HilangSetelahMati()
    {
        yield return new WaitForSeconds(1f);   
        UIGameOver.SetActive(true);
        
    }
}
