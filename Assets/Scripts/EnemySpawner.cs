using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
        [SerializeField] List<WaveConfig> waveConfigs;
        [SerializeField] int startingWave = 0;
        [SerializeField] bool looping = false;
        [SerializeField] Transform enemyParentObject;


    // Start is called before the first frame update
    //IEnumerator Start()
    //    {
    //        //do
    //        //{
    //        //    yield return StartCoroutine(SpawnAllWaves());
    //        //} while (looping);

    //    }


        private IEnumerator SpawnAllWaves()
        {
            for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
            {
                var currentWave = waveConfigs[waveIndex];
                yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            }
        }


        private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
        {
        Debug.Log("SpawnAllEnemies number of enmies = " + waveConfig.GetNumberOfEnemies().ToString());
            for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
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
        Debug.Log("enemySpanwer trigger wave");
        var waveToTrigger = waveConfigs[waveToTriggerIndex];
        StartCoroutine(SpawnAllEnemiesInWave(waveToTrigger));
    }
}
