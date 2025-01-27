using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/HealthBuff")]
public class HealthBuff : PowerUpEffect
{
    public int amount; // The amount of health to add temporarily or permanently

    // Permanent health buff
    protected override void ApplyPermanent(GameObject target)
    {
        // Access the PlayerHealth component and apply healing
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(amount); // Add health permanently
        }
      
    }

    // Get the current health (for temporary effects)
    protected override float GetOriginalValue(GameObject target)
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            return playerHealth.currentHealth; // Return the current health
            
        }
        return 0f; // Default fallback value
    }

    // Set the modified health value (used for temporary effects)
    protected override void SetValue(GameObject target, float value)
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.currentHealth = Mathf.Clamp((int)value, 0, playerHealth.maxHealth); // Ensure health stays within valid range
            playerHealth.UpdateUI(); // UPDATE UI FUNC
        }
    }

    // Return the amount to buff health by (used for temporary effects)
    protected override float GetBuffAmount()
    {
        return amount; // The boost amount defined in the ScriptableObject
    }
}
