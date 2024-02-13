using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRaycast : MonoBehaviour
{
    public float range = 100f; // Max distance the raycast will check for objects

    // Update is called once per frame
    void Update()
    {
        DetectObjects();
    }

    void DetectObjects()
    {
        RaycastHit hit;
        // Create a ray that shoots forward from the camera's position
        Ray ray = new Ray(transform.position, transform.forward);

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Looking at: " + hit.transform.name); // Log the name of the object

            // You can also trigger interactions here, e.g.:
             if (hit.transform.gameObject.CompareTag("Interactable"))
             {
                Debug.Log("Press E to record sound");
             }
        }
    }
}
