using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPad : MonoBehaviour
{
    [SerializeField] GameObject missileAmmoPrefab;
    [SerializeField] GameObject cannonAmmoPrefab;
    [SerializeField] GameObject ammoCollectionPoint;

    //member variables
    bool pickupReady = false;
    public enum AmmoTypes { Missile, Cannon};
    private Messages messages;

    //Constants
    const string MISSILEMESSAGETEXT = "Missiles Ready for Pickuo";
    const string CANNONMESSAGETEXT = "Cannon Rounds Ready for Pickup";

    // Start is called before the first frame update
    void Start()
    {
        messages = FindObjectOfType<Messages>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestAmmo(AmmoTypes ammoType)
    {
        if (!pickupReady)
        {
            CreateAmmoPickup(ammoType);
        }
    }

    public void AmmoCollected()
    {
        pickupReady = false;
    }

    private void CreateAmmoPickup(AmmoTypes ammoType)
    {
        GameObject prefabToCreate;
        string messageText = string.Empty;

        switch (ammoType)
        {
            case AmmoTypes.Cannon:
                prefabToCreate = cannonAmmoPrefab;
                messageText = CANNONMESSAGETEXT;
                break;
            case AmmoTypes.Missile:
                prefabToCreate = missileAmmoPrefab;
                messageText = MISSILEMESSAGETEXT;
                break;
            default:
                prefabToCreate = null;
                break;
        }

        if (prefabToCreate != null)
        {
            Instantiate(prefabToCreate, ammoCollectionPoint.transform.position, ammoCollectionPoint.transform.rotation);
            pickupReady = true;
            if(messages != null)
            {
                messages.DisplayMessage(messageText);
            }
        }
    }

}
