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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            Move();
            ProcessRotation();
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
        FindObjectOfType<LevelLoader>().LoadCurrentScene();
    }


}
