using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;

    [SerializeField] HealthBar healthBar;
    [SerializeField] Animator animator;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject player;

    private bool dead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
        }

        else if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(!dead)
        {
            animator.SetTrigger("Die");
            GetComponent<CharacterMovement>().enabled = false;
            dead = true;
            this.gameObject.SetActive(false);
            player.SetActive(false);
            gameOver.SetActive(true);
        }
        
    }
}
