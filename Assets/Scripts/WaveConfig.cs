using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveConfig : MonoBehaviour
{

    //config
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int startNumberOfEnemies = 1;
    [SerializeField] int maxNumberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] bool disableWhenMothershipSpawned = false;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetStartNumberOfEnemies() { return startNumberOfEnemies; }
    public int GetMaxNumberOfEnemies() { return maxNumberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }
    public bool GetDisableWhenMothershipSpawned() { return disableWhenMothershipSpawned; }
}
