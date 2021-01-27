using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    //Constants
    const string WAVETRIGGERTAG = "WaveTrigger";

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

        if (other.gameObject.tag == WAVETRIGGERTAG)
        {
            return;
        }
        if(other.gameObject.GetComponent<AmmoPickup>() != null)
        {
            PlayershipController playershipController = this.GetComponent<PlayershipController>();
            AmmoPickup ammoPickup = other.gameObject.GetComponent<AmmoPickup>();
            playershipController.AddAmmo(ammoPickup);
            return;
        }
        //StartDeathSequence();
    }

    private void StartDeathSequence()
    {

        this.GetComponent<PlayershipController>().OnPlayerDeath();
    }
}
