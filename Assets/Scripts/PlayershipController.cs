using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayershipController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In m per sec")][SerializeField] float moveSpeed = 4f;
    [Tooltip("In meters")] [SerializeField] float xMovementRange = 7f;
    [Tooltip("In meters")] [SerializeField] float yMovementRange = 4f;
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float missileFiringPeriod = 0.4f;
    [SerializeField] float cannonFiringPeriod = 0.1f;
    [SerializeField] float missileLaunchOffset = 1f;
    [SerializeField] GameObject[] missileLaunchers;
    [SerializeField] int missileAmmo = 24;
    [SerializeField] int cannonAmmo = 1000;
    [SerializeField] AudioClip cannonSFX;


    [Header("ScreenPosition")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("ControlThrow")]
    [SerializeField] float throwPitchFactor = -20f;
    [SerializeField] float throwRollFactor = -40f;

    [Header("Death")]
    [SerializeField] GameObject explosion;

    //member vairables
    float xThrow;
    float yThrow;
    bool isControlEnabled = true;
    Coroutine missileFireCoroutine;
    Coroutine cannonFireCoroutine;
    int currentMissileLauncher = 0;
    AmmoDisplay ammoDisplay;
    AmmoPad ammoPad;
    int startMissileAmmo;
    int startCannonAmmo;
    Messages messages;
    AudioSource audioSource;

    //constants
    const string MISSILEPICKIPMESSAGE = "Missiles Replenished";
    const string CANNONPICKUPMESSAGE = "Cannon Ammo Replenished";



    // Start is called before the first frame update
    void Start()
    {
        cannon.SetActive(false);
        ammoDisplay = FindObjectOfType<AmmoDisplay>();
        DisplayAmmoLevels();

        ammoPad = FindObjectOfType<AmmoPad>();
        startMissileAmmo = missileAmmo;
        startCannonAmmo = cannonAmmo;

        messages = FindObjectOfType<Messages>();

        audioSource = GetComponent<AudioSource>();
    }

    private void DisplayAmmoLevels()
    {
        if (ammoDisplay != null)
        {
            ammoDisplay.SetMissileAmmoDisaply(missileAmmo);
            ammoDisplay.SetCannonAmmoDisaply(cannonAmmo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            Move();
            ProcessRotation();
            FireCannon();
            FireMissiles();
            RequestAmmo();
        }
    }

    private void RequestAmmo()
    {
        if (cannonAmmo < startCannonAmmo / 2)
        {
            ammoPad.RequestAmmo(AmmoPad.AmmoTypes.Cannon);
        }

        if (missileAmmo < startMissileAmmo /2)
        {
            ammoPad.RequestAmmo(AmmoPad.AmmoTypes.Missile);
        }

    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = (transform.localPosition.y * positionPitchFactor);
        float pitchDueToThrow = (yThrow * throwPitchFactor);
        float pitch = pitchDueToPosition + pitchDueToThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * throwRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void Move()
    {
         xThrow = Input.GetAxis("Horizontal");
         yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * moveSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float newXpos = Mathf.Clamp(rawNewXPos, xMovementRange * -1, xMovementRange);

        float yOffset = yThrow * moveSpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float newYpos = Mathf.Clamp(rawNewYPos, yMovementRange * -1, yMovementRange);


        transform.localPosition = new Vector3(newXpos, newYpos, transform.localPosition.z);
    }

    public void OnPlayerDeath()
    {
        isControlEnabled = false;
        GameObject explode = Instantiate(explosion, transform.position, transform.rotation);
        FindObjectOfType<LevelLoader>().LoadGameOverScene();
    }

    private void FireCannon()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (cannonAmmo > 0)
            {
                cannon.SetActive(true);

            }

            cannonFireCoroutine = StartCoroutine(FireCannonContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            cannon.SetActive(false);
            StopCoroutine(cannonFireCoroutine);
            audioSource.Stop();
        }
    }

    IEnumerator FireCannonContinuously()
    {
        while (true)
        {
            if (cannonAmmo > 0)
            {
                cannonAmmo--;
                DisplayAmmoLevels();
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(cannonSFX);
                }
            }
            else
            {
                cannon.SetActive(false);
            }
            yield return new WaitForSeconds(cannonFiringPeriod);

        }

    }

    private void FireMissiles()
    {

        if (Input.GetButtonDown("Fire2"))
        {
            missileFireCoroutine = StartCoroutine(FireMissileContinuously());
        }
        if (Input.GetButtonUp("Fire2"))
        {
            StopCoroutine(missileFireCoroutine);
        }
    }

    IEnumerator FireMissileContinuously()
    {
        while (true)
        {
            if (missileAmmo > 0)
            {
                LaunchMissile();
            }
            yield return new WaitForSeconds(missileFiringPeriod);

        }

    }

    private void LaunchMissile()
    {
        Vector3 launcher = missileLaunchers[currentMissileLauncher].transform.position;
        launcher.z = launcher.z + missileLaunchOffset;
        GameObject missile = Instantiate(missilePrefab, launcher, transform.rotation) as GameObject;
        missileAmmo--;
        DisplayAmmoLevels();
        SetNextMissileLauncher();
    }

    private void SetNextMissileLauncher()
    {
        if (currentMissileLauncher < missileLaunchers.Length - 1)
        {
            currentMissileLauncher++;
        }
        else
        {
            currentMissileLauncher = 0;
        }
    }

    public void AddAmmo(AmmoPickup ammoPickup)
    {
        string ammoMessage = string.Empty;
        switch (ammoPickup.GetAmmoType())
        {
            case AmmoPickup.AmmoTypes.Missile:
                missileAmmo = startMissileAmmo;
                ammoMessage = MISSILEPICKIPMESSAGE;
                break;
            case AmmoPickup.AmmoTypes.Cannon:
                cannonAmmo = startCannonAmmo;
                ammoMessage = CANNONPICKUPMESSAGE;
                break;
            default:
                break;

        }
        DisplayAmmoLevels();
        messages.DisplayMessage(ammoMessage);
        ammoPickup.ProcessPickedUp();
        ammoPad.AmmoCollected();

    }
}
