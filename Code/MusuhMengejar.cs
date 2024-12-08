using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusuhMengejar : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private Transform player;           
    [SerializeField] private float kecepatanMengejar = 2f; 
    [SerializeField] private float jarakDeteksi = 5f;      
    [SerializeField] private float jarakKehilangan = 7f;   
    [SerializeField] private float waktuKembali = 3f;      
    [SerializeField] private float jarakKembali = 2f;      

    private Vector3 posisiAwal;                          
    private bool playerTerdekeksi = false;                
    private Animator anim;                                
    private bool isMoving = false;                        
    private Vector3 ScalaAwal;                            
    private bool isChasing = false;                       

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ScalaAwal = transform.localScale; 
        posisiAwal = transform.position;
    }

    private void Update()
    {
        if (player != null)
        {
            float jarakKePlayer = Vector3.Distance(transform.position, player.position);

            if (jarakKePlayer < jarakDeteksi)
            {
                playerTerdekeksi = true;  
                isChasing = true;         
                MengejarPlayer();
            }
            else if (jarakKePlayer > jarakKehilangan && playerTerdekeksi)
            {
               
                StartCoroutine(KembaliKePosisiAwal());
                playerTerdekeksi = false;
                isChasing = false;
            }

            if (!isMoving && !isChasing)
            {
                anim.SetBool("mengejar", false);  
                anim.SetBool("idle", true);       
            }
        }
    }

    private void MengejarPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, kecepatanMengejar * Time.deltaTime);
        isMoving = true;
        anim.SetBool("mengejar", true);  
        anim.SetBool("idle", false);     

        if (player.position.x > transform.position.x && transform.localScale.x < 0)
        {
            Flip(1);  
        }
        else if (player.position.x < transform.position.x && transform.localScale.x > 0)
        {
            Flip(-1);
        }
    }

    private IEnumerator KembaliKePosisiAwal()
    {
        yield return new WaitForSeconds(waktuKembali);

        while (Vector3.Distance(transform.position, posisiAwal) > jarakKembali)
        {
            transform.position = Vector3.MoveTowards(transform.position, posisiAwal, kecepatanMengejar * Time.deltaTime);
            isMoving = true;
            anim.SetBool("mengejar", true);  
            anim.SetBool("idle", false);     

            if (posisiAwal.x > transform.position.x)
            {
                Flip(1); 
            }
            else
            {
                Flip(-1); 
            }

            yield return null;
        }

        transform.position = posisiAwal; 
        isMoving = false;
        anim.SetBool("mengejar", false);  
        anim.SetBool("idle", true);      

        if (player != null && playerTerdekeksi)
        {
            MengejarPlayer(); 
        }

        isChasing = false;  
    }

    private void Flip(int _direction)
    {
        transform.localScale = new Vector3(Mathf.Abs(ScalaAwal.x) * -_direction, ScalaAwal.y, ScalaAwal.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, jarakDeteksi);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, jarakKehilangan);
    }
}
