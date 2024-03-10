using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        
        // Player Movement
        motor = GetComponent<PlayerMotor>();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.ResetScene.performed += ctx => motor.ResetScene();

        // Advance and reverse time
        onFoot.AdvanceTime.performed += ctx => motor.AdvanceTime(ctx);
        onFoot.AdvanceTime.canceled += ctx => motor.AdvanceTime(ctx);
        onFoot.ReverseTime.performed += ctx => motor.ReverseTime(ctx);
        onFoot.ReverseTime.canceled += ctx => motor.ReverseTime(ctx);

        // Interact with Items or NPCs
        onFoot.Interact.performed += ctx => motor.Interact();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        motor.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
