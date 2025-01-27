using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Buff", menuName = "Damage System/Damage Buff")]
public class DamageBuff : PowerUpEffect
{
    public float buffAmount; // The amount to buff the damage

    protected override void ApplyPermanent(GameObject target)
    {
        DamageDealer damageDealer = target.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            // Modify the base damage permanently
            damageDealer.ModifyDamage(buffAmount);
        }
    }

    protected override float GetOriginalValue(GameObject target)
    {
        DamageDealer damageDealer = target.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            return damageDealer.GetCurrentDamage();
        }
        return 0f;
    }
    protected override void SetValue(GameObject target, float value)
    {
        DamageDealer damageDealer = target.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            damageDealer.SetDamage(value);
        }
    }

    protected override float GetBuffAmount()
    {
        return buffAmount;
    }
}
