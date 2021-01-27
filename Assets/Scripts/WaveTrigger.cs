using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    [SerializeField] int waveToTrigger = 1;

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject.tag == "Player")
        {
            TriggerNextWave();
        }

    }

    private void TriggerNextWave()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner == null)
        {
            return;
        }
        enemySpawner.TriggerWave(waveToTrigger);

    }
}
