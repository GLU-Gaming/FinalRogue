using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] public int maxHealth = 100; // Maximum health of the player
    public int currentHealth;                   // Current health of the player
    public bool IsDead => currentHealth <= 0;    // Check if the player is dead
    [SerializeField] public Image healthBarFill; // Reference to the health bar's fill image
 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
       
    }
    public void TakeDamage(int DamageAmount)
    {
        if (IsDead) return; // Ignore damage if the player is already dead

        currentHealth -= DamageAmount; // Subtract damage from health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateUI();
    }

    public void Heal(int HealAmount)
    {
        if (IsDead) return; 

        currentHealth += HealAmount; // Add health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();
       
    }
    public void UpdateUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
   public void Die()  
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press the spacebar to simulate taking damage
        {
            TakeDamage(10); // Apply 10 damage
        }
    }
}
