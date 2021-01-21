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
        //StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        Debug.Log("Start Death Sequence");
        this.GetComponent<PlayershipController>().OnPlayerDeath();
    }
}
