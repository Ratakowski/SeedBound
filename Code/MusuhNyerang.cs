using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusuhNyerang : MonoBehaviour
{
    [SerializeField] private float CooldownNyerang;
    [SerializeField] private float Jarak;
    [SerializeField] private float JarakCollider;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask GeokiMask;

    private float WaktuCooldown = Mathf.Infinity;
    private PlayerHP NyawaGeoki;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        WaktuCooldown += Time.deltaTime;

        if (GeokiDalamHitbox())
        {
            if (WaktuCooldown >= CooldownNyerang)
            {
                WaktuCooldown = 0;
                KenaSerang();
                anim.SetTrigger("nyerang");
                SFXManager.instance.PlayHitSound();

            }
        }
    }

    private bool GeokiDalamHitbox()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * Jarak * transform.localScale.x * JarakCollider,
            new Vector3(boxCollider.bounds.size.x * Jarak, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.left,
            0,
            GeokiMask
        );

        if (hit.collider != null)
        {
            NyawaGeoki = hit.transform.GetComponent<PlayerHP>();
            if (NyawaGeoki != null)
            {
                Debug.Log($"Nyawa component found on: {NyawaGeoki.gameObject.name}");
                return true;
            }
            else
            {
                Debug.LogWarning($"Detected {hit.collider.name}, but no Nyawa component found.");
            }
        }
        else
        {
            Debug.Log("No player detected in the hitbox.");
        }

        return false;
    }

    private void KenaSerang()
    {
        if (NyawaGeoki != null)
        {
            Debug.Log($"Dealing {damage} damage to {NyawaGeoki.gameObject.name}.");
            NyawaGeoki.TerkenaSerangan(damage);
        }
        else
        {
            Debug.LogWarning("KenaSerang() called, but no valid Nyawa component found.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * Jarak * transform.localScale.x * JarakCollider,
            new Vector3(boxCollider.bounds.size.x * Jarak, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        );
    }
}
