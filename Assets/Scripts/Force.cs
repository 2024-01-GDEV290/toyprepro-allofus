using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    public float thrust = 1f;
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        if (Input.GetButton("Vertical"))
        {
            rb.AddForce(transform.forward * thrust);
        }
    }
}
