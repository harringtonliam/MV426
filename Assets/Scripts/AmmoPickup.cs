using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoTypes ammoType = AmmoTypes.Cannon;
    [SerializeField] AudioClip pickupSound;

    //member varaibles

    public enum AmmoTypes { Missile, Cannon };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AmmoTypes GetAmmoType()
    {
        return ammoType;
    }

    public void ProcessPickedUp()
    {
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        Destroy(gameObject);

    }
}
