using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Health Settings")]
    protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    [Header("Move towards player system")]
    [SerializeField] protected float stoppingDistance = 1f;
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] protected Transform playerPosition;
    [SerializeField] protected float activationRange = 20f;
    [SerializeField] protected float attackRange = 3.5f;


    [Header("Attack Cooldown Settings")]
    [SerializeField] protected float specialAttackCooldown = 5f; // Cooldown in seconds
    protected bool canSpecialAttack = true; // Protected for child class access
    [SerializeField] protected ParticleSystem deathParticles;
    [SerializeField] protected AudioClip deathSound;
    [SerializeField] protected AudioClip specialAttackSound;
protected  AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameObject.layer = LayerMask.NameToLayer(CollisionLayer.Enemy);
    }

 protected virtual void Start()
    {
        currentHealth = maxHealth;

        // Dynamically find the player's position if it's not assigned
        if (playerPosition == null)
        {
            playerPosition = GameObject.FindWithTag("Player")?.transform;
        }
    }

    private void Update()
    {
        if (playerPosition == null) return;

        // Check if enemy can attack and trigger SpecialAttack
        if (canSpecialAttack)
        {
            SpecialAttack();
        }

        // Other logic for movement or actions can be added here
    }

    public virtual void TakeDamage(int amount) // Virtual for overriding in child classes
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die() // Changed to 'protected' for overriding in children
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        audioSource.PlayOneShot(deathSound);
    }

    public virtual void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

   

    public virtual Vector2 GetPlayerPosition()
    {
        if (playerPosition != null)
        {
            return (Vector2)playerPosition.position;
        }
        else
        {
            return Vector2.zero;
        }
    }
    public virtual void SpecialAttack()
    {
      
        
    }
    public virtual IEnumerator SpecialAttackCooldown()
    {
        canSpecialAttack = false;
        yield return new WaitForSeconds(specialAttackCooldown);
        canSpecialAttack = true;
    }

    private void OnDrawGizmos()
    {
        // Draw the attack range square in 2D (XY plane)
        Gizmos.color = Color.red;
        DrawWireSquare(transform.position, attackRange);
    }

    public virtual void MoveTowardsPlayer() // Virtual for child overrides
    {
        if (playerPosition != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);
            if (distanceToPlayer > stoppingDistance)
            {
                Vector2 newPosition = Vector2.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);
                transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            }
        }
    }

    // Helper method to draw a 2D square (wireframe) on the XY plane
    private void DrawWireSquare(Vector3 center, float size)
    {
        // Half size to center the square around the origin
        float halfSize = size / 2f;

        // Define the corners of the square
        Vector3 topLeft = new Vector3(center.x - halfSize, center.y + halfSize, center.z);
        Vector3 topRight = new Vector3(center.x + halfSize, center.y + halfSize, center.z);
        Vector3 bottomLeft = new Vector3(center.x - halfSize, center.y - halfSize, center.z);
        Vector3 bottomRight = new Vector3(center.x + halfSize, center.y - halfSize, center.z);

        // Draw the lines connecting the corners
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
