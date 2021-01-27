using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
        //Parameters
        [SerializeField] List<WaveConfig> waveConfigs;
        [SerializeField] int startingWave = 0;
        [SerializeField] Transform enemyParentObject;

    //Member variables
    int[] waveTriggerCount;

    private void Start()
    {
        waveTriggerCount = new int[waveConfigs.Count];
    }

    //private IEnumerator SpawnAllWaves()
    //    {
    //        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
    //        {
    //            var currentWave = waveConfigs[waveIndex];
    //            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave, waveIndex));
    //        }
    //    }


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
        Debug.Log("SpawnAllEnemiesInWave waveindex=" + waveIndex.ToString() + " maxnumber= " + maxNumberOfEnemies + " number to spawn=" + numberOfEnemiesToSpawn.ToString());
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

        var waveToTrigger = waveConfigs[waveToTriggerIndex];
        StartCoroutine(SpawnAllEnemiesInWave(waveToTrigger, waveToTriggerIndex));
    }
}
