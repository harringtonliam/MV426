using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] Text missileAmmoText;
    [SerializeField] Text cannonAmmoText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMissileAmmoDisaply(int missileAmmo)
    {
        missileAmmoText.text = missileAmmo.ToString();
    }

    public void SetCannonAmmoDisaply(int cannonAmmo)
    {
        cannonAmmoText.text = cannonAmmo.ToString();
    }
}
