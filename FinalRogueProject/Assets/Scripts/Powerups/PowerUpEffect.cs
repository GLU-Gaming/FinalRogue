using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUpEffect", menuName = "PowerUps/PowerUpEffect")]
public abstract class PowerUpEffect : ScriptableObject
{

    public enum BuffType
    {
        Temporary,
        Permanent
    }
    public void Apply(GameObject target)
    {
        if (buffType == BuffType.Permanent)
        {
            ApplyPermanent(target);
        }
        else if (buffType == BuffType.Temporary)
        {
            target.GetComponent<MonoBehaviour>().StartCoroutine(HandleTemporaryEffect(target));
        }
    }

    public BuffType buffType;
    public float tempDuration;

    protected abstract void ApplyPermanent(GameObject target);
    protected abstract float GetOriginalValue(GameObject target); // Get the value to modify
    protected abstract void SetValue(GameObject target, float value); 




    private IEnumerator HandleTemporaryEffect(GameObject target)
    {
        float originalValue = GetOriginalValue(target);

        // Apply the temporary buff
        SetValue(target, originalValue + GetBuffAmount());

        // Wait for the temporary duration
        yield return new WaitForSeconds(tempDuration);

        // Restore the original value
        SetValue(target, originalValue);
    }

  
    protected virtual float GetBuffAmount()
    {
        return 0f;
    }
}
    