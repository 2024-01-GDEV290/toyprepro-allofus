using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleJumpController : MonoBehaviour
{
    public float jumpForce = 10f;
    private bool isGrounded;

    private void Update()
    {
        // Handle player jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is grounded (touching the ground)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
