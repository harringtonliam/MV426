using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    //Parameters
    [SerializeField] int damage = 1;
    [SerializeField] GameObject explosionFX;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.GetComponent<Health>() != null)
        {
            DealDamage(otherGameObject);
        }

        DestroyMissile();
    }


    private void DealDamage(GameObject otherGameObject)
    {
        otherGameObject.GetComponent<Health>().TakeDamage(damage);
    }

    private void DestroyMissile()
    {
        if (explosionFX != null)
        {
            Instantiate(explosionFX, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

}
