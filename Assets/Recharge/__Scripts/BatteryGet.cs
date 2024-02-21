using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryGet : MonoBehaviour
{
    public TMP_Text remainingPower;
    public int powerRemaining = 100;

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    private void OnTriggerEnter(Collider other)
    {
        // Delete the colliding object
        Destroy(other.gameObject);
        
        // Update the UI text
        
    }

    private void Start()
    {
        // Set up the initial UI text
        remainingPower.text = remainingPower.text + powerRemaining;
        DrainPower();

    }

    private void DrainPower()
    {
        for (int i = 100; i >= powerRemaining;)
        {
            i--;
            powerRemaining = i;
        }
    }
}
