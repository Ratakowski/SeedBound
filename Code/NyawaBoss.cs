using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyawaBoss : MonoBehaviour
{
    [SerializeField] private float NyawaAwal = 10f;
    public float NyawaTersisa;
    private Animator anim;
    private bool Mati;

    private MusuhMengejar musuhMengejar;
    private MusuhNyerang musuhNyerang;

    private void Awake()
    {
        NyawaTersisa = NyawaAwal;
        Debug.Log($"{gameObject.name} initial health: {NyawaTersisa}");
        anim = GetComponent<Animator>();
        musuhMengejar = GetComponent<MusuhMengejar>();
        musuhNyerang = GetComponent<MusuhNyerang>();
    }

    public void TerkenaSerangan(float _damage)
    {
        NyawaTersisa = Mathf.Clamp(NyawaTersisa - _damage, 0, NyawaAwal);
        Debug.Log($"{gameObject.name} terkena serangan! Sisa nyawa: {NyawaTersisa}");

        if (NyawaTersisa <= 0 && !Mati)
        {
            Mati = true;
            if (anim != null)
                anim.SetTrigger("Mati");
            Debug.Log($"{gameObject.name} has died.");

            if (musuhMengejar != null)
            {
                musuhMengejar.enabled = false;
            }

            if (musuhNyerang != null)
            {
                musuhNyerang.enabled = false;
            }

            PointCollectorBoss poincollectorboss = FindObjectOfType<PointCollectorBoss>();
            if (poincollectorboss != null)
            {
                poincollectorboss.BossDefeated();
            }
            else
            {
                Debug.LogWarning("PoinCollector tidak ditemukan di scene.");
            }

            StartCoroutine(HilangSetelahMati());
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
