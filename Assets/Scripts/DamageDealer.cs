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


    public int GetDamage()
    {
        return damage;
    }


    public void Hit()
    {
        DestroyDamageDealer();
    }

    private void DestroyDamageDealer()
    {
        if (explosionFX != null)
        {
           GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            if (transform.parent != null)
            {
                explosion.transform.parent = transform.parent;
            }
        }
        Destroy(gameObject);
    }

}
