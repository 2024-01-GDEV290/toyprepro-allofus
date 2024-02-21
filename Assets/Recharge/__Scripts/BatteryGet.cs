using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryGet : MonoBehaviour
{
    public TMP_Text powerText;
    private int powerRemaining = 100;

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
        if (powerText != null)
        {
            powerText.text = "Power Remaining: " + powerRemaining;
        }
    }

    private void Start()
    {
        // Set up the initial UI text
        UpdatePowerText();
    }
}
