using UnityEngine;

public class DamageDealer : MonoBehaviour, IDamageDealer
{
    [SerializeField] private DamageData damageData;
    private float currentDamage;

    private void Start()
    {
        currentDamage = damageData.damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if this is an enemy projectile hitting an enemy, or player projectile hitting player
        if (gameObject.layer == LayerMask.NameToLayer(CollisionLayer.EnemyProjectile) &&
            collision.gameObject.layer == LayerMask.NameToLayer(CollisionLayer.Enemy))
            return;

        if (gameObject.layer == LayerMask.NameToLayer(CollisionLayer.PlayerProjectile) &&
            collision.gameObject.layer == LayerMask.NameToLayer(CollisionLayer.Player))
            return;

        ApplyDamage(collision.gameObject);
    }

    public void ApplyDamage(GameObject target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageData.damageAmount);
            if (damageData.destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ModifyDamage(float amount)
    {
        currentDamage += amount;
    }

    public float GetCurrentDamage()
    {
        return currentDamage;
    }

    public void SetDamage(float value)
    {
        currentDamage = value;
    }
}