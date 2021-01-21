using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    [SerializeField] int waveToTrigger = 1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("wave trigger on trigger enter");
        GameObject otherObject = other.gameObject;
        Debug.Log("wave trigger on trigger enter" + otherObject.tag);
        if (otherObject.tag == "Player")
        {
            //TODO  Trigger a wave
            Debug.Log("wave trigger on if test player");
            TriggerNextWave();
        }

    }

    private void TriggerNextWave()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner == null)
        {
            Debug.Log("trigger next wave enemy spawner = null");
            return;
        }
        Debug.Log("trigger next wave enemy spawner found");
        enemySpawner.TriggerWave(waveToTrigger);

    }
}
