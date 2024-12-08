using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusuhPatroli : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform UjungKiri;
    [SerializeField] private Transform UjungKanan;

    [Header("Enemy")]
    [SerializeField] private Transform Musuh;

    [Header("Movement Parameters")]
    [SerializeField] private float Kecepatan = 2f;
    private Vector3 ScalaAwal;
    private bool GerakKeKiri = true;
    private bool Mati = false;

    [Header("Idle Behavior")]
    [SerializeField] private float WaktuNyantai = 1f;
    private float TimerNyantai;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        ScalaAwal = Musuh.localScale;

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Mati || Musuh == null)
        {
            return;
        }

        if (GerakKeKiri)
        {
            if (Musuh.position.x > UjungKiri.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                IdleAtPoint();
            }
        }
        else
        {
            if (Musuh.position.x < UjungKanan.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                IdleAtPoint();
            }
        }
    }

    private void MoveInDirection(int _direction)
    {
        TimerNyantai = 0;

        if (anim != null)
            anim.SetBool("jalan", true);

        Musuh.localScale = new Vector3(
            Mathf.Abs(ScalaAwal.x) * -_direction,
            ScalaAwal.y,
            ScalaAwal.z
        );

        Musuh.position = new Vector3(
            Musuh.position.x + Time.deltaTime * _direction * Kecepatan,
            Musuh.position.y,
            Musuh.position.z
        );
    }

    private void IdleAtPoint()
    {
        if (anim != null)
            anim.SetBool("jalan", false);

        TimerNyantai += Time.deltaTime;

        if (TimerNyantai >= WaktuNyantai)
        {
            GerakKeKiri = !GerakKeKiri;
        }
    }

    public void SetMati()
    {
        Mati = true;
        Debug.Log($"{gameObject.name} patrol logic halted due to death.");

        if (anim != null)
            anim.SetBool("jalan", false);
    }
}
