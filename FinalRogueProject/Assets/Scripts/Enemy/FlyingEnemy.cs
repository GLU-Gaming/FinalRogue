using System.Collections;
using UnityEngine;

public class FlyingEnemy : BaseEnemy, IDamageable
{
    [Header("Flying Movement Settings")]
    [SerializeField] private float minY = -8.41f;
    [SerializeField] private float maxY = -2.41f;
    [SerializeField] private float yMoveSpeed = 2f;
    private bool isMovingToRandomY = false;

    [Header("Attack Settings")]
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float projectileDamage = 35f;
    [SerializeField] protected int projectileCount = 3;
    [SerializeField] protected float projectileSpeed = 5f;
    [SerializeField] protected float spawnDistance = 1f;
    

    private void OnEnable()
    {
        if (playerPosition == null)
        {
            playerPosition = GameObject.FindWithTag("Player")?.transform;
        }
        canSpecialAttack = true;
    }

    private void Update()
    {
        if (playerPosition == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);

        if (distanceToPlayer <= activationRange)
        {
            if (distanceToPlayer > stoppingDistance)
            {
                FlyingEnemyMovement();
            }
            else
            {
                RandomY();
                if (canSpecialAttack)
                {
                    SpecialAttack();
                }
            }
        }
    }

    public override void SpecialAttack()
    {
        if (canSpecialAttack && projectilePrefab != null && projectileSpawnPoint != null)
        {
            Debug.Log("Flying Enemy Special Attack triggered!");
            StartCoroutine(SpawnProjectiles());
            StartCoroutine(SpecialAttackCooldown());
            if (audioSource != null && specialAttackSound != null)
            {
                audioSource.PlayOneShot(specialAttackSound);
            }
        }
    }

    public override  IEnumerator SpecialAttackCooldown()
    {
        canSpecialAttack = false;
        yield return new WaitForSeconds(specialAttackCooldown);
        canSpecialAttack = true;
    }

    private IEnumerator SpawnProjectiles()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is not assigned!");
            yield break;
        }

        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 spawnPosition = projectileSpawnPoint.position;

            // Create the projectile
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

            if (projectile != null)
            {
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = ((Vector2)playerPosition.position - (Vector2)projectileSpawnPoint.position).normalized;
                    rb.velocity = direction * projectileSpeed;
                    Debug.Log($"Projectile {i + 1} spawned at {spawnPosition} with velocity {rb.velocity}");
                }
                else
                {
                    Debug.LogError("Projectile prefab must have a Rigidbody2D component!");
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
    private void RandomY()
    {
        if (!isMovingToRandomY)
        {
            StartCoroutine(MoveToRandomY());
        }
    }

    private IEnumerator MoveToRandomY()
    {
        isMovingToRandomY = true;

        float randomY = Random.Range(minY, maxY);

        while (Mathf.Abs(transform.position.y - randomY) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, randomY, transform.position.z),
                yMoveSpeed * Time.deltaTime
            );
            yield return null;
        }

        isMovingToRandomY = false;
    }

    private void FlyingEnemyMovement()
    {
        Vector3 targetPosition = Vector3.MoveTowards(
            transform.position,
            new Vector3(playerPosition.position.x, transform.position.y, transform.position.z),
            moveSpeed * Time.deltaTime
        );
        transform.position = targetPosition;

        if (!isMovingToRandomY)
        {
            StartCoroutine(MoveToRandomY());
        }
    }
}
