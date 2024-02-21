using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 5f; // Increased mouse sensitivity
    public float jumpForce = 10f;

    private float verticalRotation = 0f;
    private bool isGrounded;
    private bool isPlayerInputEnabled = true;

    private void Update()
    {
        if (isPlayerInputEnabled)
        {
            // Handle player movement
            MovePlayer();

            // Handle player rotation (looking around)
            RotatePlayer();

            // Handle player jumping
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * movementSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX * mouseSensitivity);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    public void SetPlayerInputEnabled(bool isEnabled)
    {
        isPlayerInputEnabled = isEnabled;
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
