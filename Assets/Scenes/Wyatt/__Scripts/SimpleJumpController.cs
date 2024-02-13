using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleJumpController : MonoBehaviour
{
    public float jumpForce = 10f;
    private bool isGrounded;
    public Animator animator;

    private void Update()
    {
        // Handle player jumping

        if (!isGrounded)
        {
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }

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
