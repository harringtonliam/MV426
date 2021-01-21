using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    //Parameters
    [SerializeField] float missileSpeed = 10f;
   
    //Member Variables
    Rigidbody m_Rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            m_Rigidbody.velocity = transform.forward * missileSpeed;
    }
}
