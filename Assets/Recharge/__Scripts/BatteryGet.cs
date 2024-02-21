using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryGet : MonoBehaviour
{
    public TMP_Text powerText;
    public int powerRemaining = 100;

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    private void OnTriggerEnter(Collider other)
    {
        // Delete the colliding object
        Destroy(other.gameObject);
        
        // Update the UI text
        UpdatePowerText();
    }

    private void UpdatePowerText()
    {
        // Update the UI text with the remaining power value
        powerText.text = powerText.text + powerRemaining;
    }

    private void Start()
    {
        // Set up the initial UI text
        UpdatePowerText();

        DrainPower();
    }

    private void DrainPower()
    {
        for (int i = 100; i >= powerRemaining && i != 0;)
        {
            i--;
            powerRemaining = i;
        }
    }

    private void Update()
    {
        UpdatePowerText();
    }
}
