using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] List<Item> inventory;
    
    [Header("Walk")]
    private Vector3 playerVelocity;
    [SerializeField] private float speed = 5.0f;

    [Header("Jump")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Interact")]
    [SerializeField] float interactRange = 1f;
    [SerializeField] LayerMask interactiveObjectLayer;
    [SerializeField] GameObject interactionTarget;
    [SerializeField] GameEventTrigger withinInteractRange;
    [SerializeField] GameEventTrigger outOfInteractRange;

    [Header("Camera/Look")]
    [SerializeField] private Camera cam;
    private float xRotation = 0.0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    [Header("Advance/Reverse time")]
    [SerializeField] Crank crank; 

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        inventory = new List<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        
    }
    private void FixedUpdate()
    {
        CheckInteractionTarget();
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0) playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    public void CheckInteractionTarget()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit, interactRange, interactiveObjectLayer))
        {
            Debug.Log("You are within interact range of an interactive object.");
            if (interactionTarget != hit.transform.gameObject)
            {
                interactionTarget = hit.transform.gameObject;
                withinInteractRange.Raise();

            }

        } else if (interactionTarget != null)
            {
                interactionTarget = null;
                outOfInteractRange.Raise();
            }
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactRange, Color.green);
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // calculate camera rotation for looking up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);

        // Rotate player to look horizontally
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
        }
    }

    public void Interact()
    {
        if (!interactionTarget) return;
        ItemAvatar targetItem = interactionTarget.GetComponent<ItemAvatar>();
        if (targetItem)
        {
            CollectItem(targetItem);
        }
    }

    void CollectItem(ItemAvatar targetItem)
    {
        interactionTarget = null;
        outOfInteractRange.Raise();
        inventory.Add(targetItem.Collect());
    }
    // Advance and reverse time should eventually fire events, but just wiring them directly to the crank for now. 
    public void AdvanceTime(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && crank.timeState == ETimeState.Idle)
        {
            crank.timeState = ETimeState.WindingForward;
        } else if (ctx.canceled && crank.timeState == ETimeState.WindingForward)
        {
            crank.StartTicking();
        }

    }

    public void ReverseTime(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && crank.timeState == ETimeState.Idle)
        {
            crank.timeState = ETimeState.WindingReverse;
        }
        else if (ctx.canceled && crank.timeState == ETimeState.WindingReverse)
        {
            crank.StartTicking();
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

 

}
