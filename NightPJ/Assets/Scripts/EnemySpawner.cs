using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public static EnemySpawner instance;
    public Transform room1Parent; 
    public Transform room2Parent;
    public Transform room3Parent; 
    public Transform room4Parent; 

    private Transform[] currentSpawnPoints; 
    public int totalEnemiesToSpawn; 
    private int enemiesSpawned; 
    private List<GameObject> activeEnemies = new List<GameObject>(); 

    private void Awake()
    {
        SetActiveRoom(1); 
        totalEnemiesToSpawn = 7;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DecrementTotalEnemies()
{
    if (totalEnemiesToSpawn > 0)
    {
        totalEnemiesToSpawn--;

        if (enemiesSpawned > 0)
        {
            enemiesSpawned--;
        }

        if (GameHandler.instance != null)
        {
            GameHandler.instance.UpdateEnemyCount();
        }
    }
}

    private void Update()
{
    if (activeEnemies.Count < 9 && enemiesSpawned < totalEnemiesToSpawn)
    {
        int enemiesToSpawn = 9 - activeEnemies.Count;
        SpawnEnemies(enemiesToSpawn);
    }

    int beforeCount = activeEnemies.Count;
    activeEnemies.RemoveAll(enemy => enemy == null);
    int afterCount = activeEnemies.Count;

    if (beforeCount != afterCount)
    {
        Debug.Log($"Removed {beforeCount - afterCount} destroyed enemies from activeEnemies list. Remaining active enemies: {afterCount}");
    }
}

    public void SetActiveRoom(int roomNumber)
{
    switch (roomNumber)
    {
        case 1:
            currentSpawnPoints = GetChildTransforms(room1Parent);
            totalEnemiesToSpawn = 7;
            break;
        case 2:
            currentSpawnPoints = GetChildTransforms(room2Parent);
            totalEnemiesToSpawn = 10;
            break;
        case 3:
            currentSpawnPoints = GetChildTransforms(room3Parent);
            totalEnemiesToSpawn = 15;
            break;
        case 4:
            currentSpawnPoints = GetChildTransforms(room4Parent);
            totalEnemiesToSpawn = 20;
            break;
        default:
            currentSpawnPoints = null;
            totalEnemiesToSpawn = 0;
            break;
    }

    enemiesSpawned = 0;
    activeEnemies.Clear();
}

    public void SpawnEnemies(int enemyCount)
{
    if (currentSpawnPoints == null || currentSpawnPoints.Length == 0)
    {
        return;
    }

    for (int i = 0; i < enemyCount; i++)
    {
        if (enemiesSpawned >= totalEnemiesToSpawn)
        {
            break;
        }

        Transform spawnPoint = currentSpawnPoints[enemiesSpawned % currentSpawnPoints.Length];
        GameObject prefabToSpawn = Random.value > 0.5f ? enemyPrefab1 : enemyPrefab2; 
        GameObject spawnedEnemy = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        activeEnemies.Add(spawnedEnemy); 
        enemiesSpawned++;

    }
}

    private Transform[] GetChildTransforms(Transform parent)
    {
        if (parent == null)
        {
            return new Transform[0];
        }

        Transform[] allTransforms = parent.GetComponentsInChildren<Transform>();
        Transform[] childTransforms = new Transform[allTransforms.Length - 1];
        for (int i = 1; i < allTransforms.Length; i++) 
        {
            childTransforms[i - 1] = allTransforms[i];
        }
        return childTransforms;
    }
}