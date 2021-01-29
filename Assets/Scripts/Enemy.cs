using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    //Parameters
    [SerializeField] int score = 1;
    [SerializeField] int particleColDamage = 1;
    [Tooltip("In Seconds")] [SerializeField] float minTimeBetweenShots = 0.2f;
    [Tooltip("In Seconds")] [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float missileLaucherOffest = 5f;
    [SerializeField] int health = 1;
    [SerializeField] GameObject deathFX;


    //member vaiables
    private float fireCountDown;



    // Start is called before the first frame update
    void Start()
    {

        fireCountDown = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider enemyBoxCollider =  gameObject.AddComponent<BoxCollider>();
        enemyBoxCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndFire();
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
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddScore(score);

        if (deathFX != null)
        {
            Instantiate(deathFX, transform.position, transform.rotation);

        }
        Destroy(gameObject);
    }

    private void CountDownAndFire()
    {
        fireCountDown -= Time.deltaTime; //decrease the shotCounter every frame and make it frame rate independant using time.Deltatime;
        if (fireCountDown <= 0f)
        {
            Fire();
        }
    }

    private void Fire()
    {
        Vector3 missileLauncher = transform.position;
        missileLauncher.y = missileLauncher.y + missileLaucherOffest;
        GameObject missile = Instantiate(missilePrefab, missileLauncher, transform.rotation);
        missile.transform.parent = transform.parent;
        fireCountDown = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    }
}
