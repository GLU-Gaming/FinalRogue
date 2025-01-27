using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WaveEnemy
{
    public GameObject enemyPrefab;
    public int count;  // Keep lowercase to match usage
}

[System.Serializable]
public class Wave
{
    public List<WaveEnemy> enemies;
    public PowerUpEffect[] availablePowerUps;
    public float timeBetweenSpawns = 2f;
    public float difficultyMultiplier = 1f;
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<Wave> waves;
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private int healAmountPerWave = 20;

    private int currentWaveIndex = 0;
    private int enemiesRemainingToSpawn;
    private int enemiesRemainingAlive;
    private bool isSpawning = false;

    // Delegates
    public delegate void WaveCompletedHandler();
    public delegate void WaveStartedHandler(int waveNumber);

    // Delegate events
    public event WaveCompletedHandler OnWaveCompleted;
    public event WaveStartedHandler OnWaveStarted;

    private void Start()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];

            // Calculate total enemies for this wave
            enemiesRemainingToSpawn = 0;
            foreach (WaveEnemy enemy in currentWave.enemies)
            {
                enemiesRemainingToSpawn += enemy.count;
            }
            enemiesRemainingAlive = enemiesRemainingToSpawn;

            // Trigger wave start event
            if (OnWaveStarted != null)
            {
                OnWaveStarted(currentWaveIndex + 1);
            }

            StartCoroutine(SpawnWave(currentWave));
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        foreach (WaveEnemy waveEnemy in wave.enemies)
        {
            for (int i = 0; i < waveEnemy.count; i++)
            {
                SpawnEnemy(waveEnemy.enemyPrefab);
                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }
        }

        isSpawning = false;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[spawnIndex];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        IDamageable damageable = enemy.GetComponent<IDamageable>();
        if (damageable != null)
        {
            StartCoroutine(WaitForEnemyDeath(enemy));
        }
    }

    private IEnumerator WaitForEnemyDeath(GameObject enemy)
    {
        yield return new WaitUntil(() => enemy == null);

        enemiesRemainingAlive--;
        CheckWaveCompletion();
    }

    private void CheckWaveCompletion()
    {
        if (enemiesRemainingAlive <= 0 && !isSpawning)
        {
            if (OnWaveCompleted != null)
            {
                OnWaveCompleted();
            }
            StartCoroutine(PrepareNextWave());
        }
    }

    private IEnumerator PrepareNextWave()
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmountPerWave);
        }

        Wave currentWave = waves[currentWaveIndex];
        if (currentWave.availablePowerUps != null && currentWave.availablePowerUps.Length > 0)
        {
            OfferPowerUps(currentWave.availablePowerUps);
        }

        yield return new WaitForSeconds(timeBetweenWaves);

        currentWaveIndex++;
        StartNextWave();
    }

    private void OfferPowerUps(PowerUpEffect[] powerUps)
    {
        if (powerUps.Length > 0)
        {
            powerUps[0].Apply(playerHealth.gameObject);
        }
    }
}

public class WaveUI : MonoBehaviour
{
    [SerializeField] private Text waveText;
    [SerializeField] private WaveManager waveManager;

    private void OnEnable()
    {
        if (waveManager != null)
        {
            waveManager.OnWaveStarted += UpdateWaveUI;
        }
    }

    private void OnDisable()
    {
        if (waveManager != null)
        {
            waveManager.OnWaveStarted -= UpdateWaveUI;
        }
    }

    private void UpdateWaveUI(int waveNumber)
    {
        if (waveText != null)
        {
            waveText.text = $"Wave {waveNumber}";
        }
    }
}