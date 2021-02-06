using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Parameters
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] Transform enemyParentObject;
    [SerializeField] int mothershipWaveIndex = 6;
    [SerializeField] int mothershipTriggerScore = 1000;

    //Member variables
    int[] waveTriggerCount;
    GameSession gameSession;

    private void Start()
    {
        waveTriggerCount = new int[waveConfigs.Count];
        gameSession = FindObjectOfType<GameSession>();
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig, int waveIndex)
    {

        int maxNumberOfEnemies = waveConfig.GetMaxNumberOfEnemies();
        int startNumberOfEnemies = waveConfig.GetStartNumberOfEnemies();
        int numberOfEnemiesToSpawn = startNumberOfEnemies + waveTriggerCount[waveIndex];

        if (numberOfEnemiesToSpawn >= maxNumberOfEnemies)
        {
            numberOfEnemiesToSpawn = maxNumberOfEnemies;
            waveTriggerCount[waveIndex] = 0;
        }
        else
        {
            waveTriggerCount[waveIndex]++;
        }
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                //TODO if you use an actuall enemy instead of prefab then this will fail if the actual ememy gets destroyed    
                var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, waveConfig.GetWayPoints()[0].transform.rotation);
                newEnemy.transform.parent = enemyParentObject;
                newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
                yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
            }

        }

    public void TriggerWave(int waveToTriggerIndex)
    {
        if(gameSession.TriggerScore >= mothershipTriggerScore && !IsMotherShipPresent())
        {
            waveToTriggerIndex = mothershipWaveIndex;
        }

        var waveToTrigger = waveConfigs[waveToTriggerIndex];
        if (IsMotherShipPresent() && waveToTrigger.GetDisableWhenMothershipSpawned())
        {
            return;
        }
        StartCoroutine(SpawnAllEnemiesInWave(waveToTrigger, waveToTriggerIndex));
    }

    private bool IsMotherShipPresent()
    {
        bool mothershipPresent = false;
        if(FindObjectOfType<EnemyMothership>() != null)
        {
            mothershipPresent = true;
        }

        return mothershipPresent;
    }
}
