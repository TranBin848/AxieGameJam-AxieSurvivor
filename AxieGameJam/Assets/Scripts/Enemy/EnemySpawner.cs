using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]

    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota; //The total number of enemies to spawn in this wave
        public float spawnInterval; //The interval at which to spawn enemies 
        public int spawnCount; //The number of enemies already spawned in this wave

    }
    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }

    public List<Wave> waves;
    public int currentWaveCount;

    [Header("Spawner Attributes")]

    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval;
    public bool isWaveActive = false;

    [Header("Player")]
    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    
    void Update()
    {
        
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive )
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        isWaveActive = true;

         yield return new WaitForSeconds(waveInterval);
         if (currentWaveCount < waves.Count - 1)
        {
            isWaveActive = false;
            currentWaveCount++;
            CalculateWaveQuota() ;
            
        }
        
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;

        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }
    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {                   

                    Instantiate(enemyGroup.enemyPrefab, player.position + new Vector3(Random.Range(-20.0f, 20.0f), player.position.y, Random.Range(-30.0f, 30.0f)), Quaternion.identity);
                  

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;

                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                }
            }
        }
        
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
            
        }
    }
}
