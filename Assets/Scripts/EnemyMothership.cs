using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMothership : MonoBehaviour
{
    [SerializeField] GameObject[] guns;
    [SerializeField] int score = 250;
    [SerializeField] int particleColDamage = 1;
    [Tooltip("In Seconds")] [SerializeField] float minTimeBetweenShots = 0.2f;
    [Tooltip("In Seconds")] [SerializeField] float maxTimeBetweenShots = 1f;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] int health = 80;
    [SerializeField] GameObject deathFX;


    //member vaiables
    private float fireCountDown;

    // Start is called before the first frame update
    void Start()
    {
        fireCountDown = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
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
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.AddScore(score);
        gameSession.TriggerScore = 0;

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

        foreach (GameObject gun in guns)
        {
            Vector3 missileLauncher = gun.transform.position;
            GameObject missile = Instantiate(missilePrefab, missileLauncher, gun.transform.rotation);
            missile.transform.parent = transform.parent;
        }
        fireCountDown = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);



    }
}
