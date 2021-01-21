using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Parameters
    [SerializeField] int health = 1;
    [SerializeField] GameObject deathFX;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        if (health <=0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (deathFX != null)
        {
            Instantiate(deathFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
