using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    float vertical = 0;
    float horizontal = 0;


    // Both input axis thresholds for the vertical and horizontal axis
    // are between -1 and 0.
    double lowerThreshold = -1;
    double upperThreshold = -0.2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        Debug.Log("Vertical Axis: " + vertical);
        Debug.Log("Horizontal Axis: " + horizontal);

        // Detection for left-hand swim (using thresholds from global vars above)
        if (vertical >= lowerThreshold && vertical <= upperThreshold)
        {
            if (horizontal >= lowerThreshold && horizontal <= upperThreshold)
            {
                Debug.Log("Good!");
            }
        }
    }
}
