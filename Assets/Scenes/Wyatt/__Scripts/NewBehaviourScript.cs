using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    // This method is called when the collider of this GameObject enters another collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other GameObject has the "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            // Destroy the current GameObject
            Destroy(gameObject);
        }
    }
}
