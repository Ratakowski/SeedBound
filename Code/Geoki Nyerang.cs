using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeokiNyerang : MonoBehaviour
{
    [SerializeField] private float WaktuPedang;
    [SerializeField] private float WaktuNyerang;
    [SerializeField] private Transform PosisiNyerang;
    [SerializeField] private float JarakNyerang;
    [SerializeField] private LayerMask MusuhGeoki;
    [SerializeField] private int damage;

    private Animator anim;
    private CharacterMovement Gerakan;
    private float WaktuCD = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Gerakan = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        WaktuCD += Time.deltaTime;

        if (WaktuPedang <= 0)
            Pedang();
        else
            WaktuPedang -= Time.deltaTime;
    }

    private void Pedang()
    {
        if (Input.GetMouseButton(0) && Gerakan.BisaNyerang())
        {
            anim.SetTrigger("pedang");
            WaktuPedang = WaktuNyerang;
            Collider2D[] musuhUntukNyerang = Physics2D.OverlapCircleAll(PosisiNyerang.position, JarakNyerang, MusuhGeoki);
            Debug.Log("Detected colliders: " + musuhUntukNyerang.Length);
            bool musuhTerdeteksi = false;

            foreach (Collider2D collider in musuhUntukNyerang)
            {
                Nyawa musuhNyawa = collider.GetComponent<Nyawa>();
                NyawaBoss musuhNyawaBoss = collider.GetComponent<NyawaBoss>();

                if (musuhNyawa != null)
                {
                    musuhNyawa.TerkenaSerangan(damage);
                    SFXManager.instance.PlaySwordHitSound();
                    Debug.Log($"Enemy hit! Damage dealt: {damage} to {collider.name}");
                    musuhTerdeteksi = true;
                }
                else if (musuhNyawaBoss != null)
                {
                    musuhNyawaBoss.TerkenaSerangan(damage);
                    SFXManager.instance.PlaySwordHitSound();
                    Debug.Log($"Boss hit! Damage dealt: {damage} to {collider.name}");
                    musuhTerdeteksi = true;
                }
                else
                {
                    Debug.LogWarning($"Collider detected but missing Nyawa or NyawaBoss component: {collider.name}");
                }
            }
            if (!musuhTerdeteksi)
            {
                SFXManager.instance.PlaySwordMissSound();
                Debug.Log("Sword missed!");
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PosisiNyerang.position, JarakNyerang);
    }
}
