using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryGet : MonoBehaviour
{
    public TMP_Text powerText;
    public int powerRemaining = 30;
    private float timer = 0.5f;

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    private void OnTriggerEnter(Collider other)
    {
        // Delete the colliding object
        Destroy(other.gameObject);
        powerRemaining += 5;
        if (powerRemaining > 30)
        {
            powerRemaining = 30;
        }
        // Update the UI text
        UpdatePowerText();
    }

    private void UpdatePowerText()
    {
        // Update the UI text with the remaining power value
        powerText.text = powerRemaining.ToString ("0");
    }

    private void Start()
    {
        // Set up the initial UI text
        UpdatePowerText();

        DrainPower();
    }

    private void DrainPower()
    {
        powerRemaining--;
        UpdatePowerText();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            DrainPower();
            timer = .5f;
        }
    }
}
