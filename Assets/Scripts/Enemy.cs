using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Parameters
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parentObject;
    [SerializeField] int score = 1;
    [SerializeField] int health = 10;
    [SerializeField] int particleColDamage = 1;
    [Tooltip("In Seconds")] [SerializeField] float rateOfFire = 3f;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float missileLaucherOffest = 5f;

    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
        StartCoroutine("Fire");
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider enemyBoxCollider =  gameObject.AddComponent<BoxCollider>();
        enemyBoxCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        BulletDamage();

    }

    private void BulletDamage()
    {
        health = health - particleColDamage;
        if (health <= 0)
        {
            EnemyDeath();
        }
    }

    private  void EnemyDeath()
    {
        GameObject enemyDeath = Instantiate(deathFX, transform.position, transform.rotation);
        enemyDeath.transform.parent = parentObject;

        FindObjectOfType<ScoreBoard>().AddScore(score);

        Destroy(gameObject);
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(rateOfFire);
        Vector3 missileLauncher = transform.position;
        missileLauncher.y = missileLauncher.y + missileLaucherOffest;
        GameObject missile = Instantiate(missilePrefab, missileLauncher, transform.rotation);
        missile.transform.parent = parentObject;

    }
}
