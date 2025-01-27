using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBuff")]
public class SpeedBuff : PowerUpEffect
{
    public float amount; // The speed boost amount

    // Apply a permanent speed boost
    protected override void ApplyPermanent(GameObject target)
    {
        Adventurer adventurer = target.GetComponent<Adventurer>();
        if (adventurer != null)
        {
            adventurer.runSpeed += amount; 
        }
        else
        {
        }
    }

    // Get the original runSpeed value (for temporary effects)
    protected override float GetOriginalValue(GameObject target)
    {
        Adventurer adventurer = target.GetComponent<Adventurer>();
        if (adventurer != null)
        {
            return adventurer.runSpeed; 
        }

        return 0f;
    }


    protected override void SetValue(GameObject target, float value)
    {
        Adventurer adventurer = target.GetComponent<Adventurer>();
        if (adventurer != null)
        {
            adventurer.runSpeed = value;
        }
        else
        {
            Debug.LogWarning("Adventurer component not found on target!");
        }
    }

   
    protected override float GetBuffAmount() // from inspector
    {
        return amount; 
    }
}
