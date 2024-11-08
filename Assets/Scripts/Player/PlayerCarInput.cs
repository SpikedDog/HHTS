using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarInput : MonoBehaviour
{
    public PlayerControls input;
    public float throttleInput;
    public float steeringInput;
    public float handBrakeInput;

    // Start is called before the first frame update
    void Awake()
    {
        input = new PlayerControls();
    }

    // When object is enabled, input system will manage script functions
    private void OnEnable()
    {
        input.Enable();
        input.MovementCommands.Throttle.performed += ApplyThrottle;
        input.MovementCommands.Throttle.canceled += ReleaseThrottle;
        input.MovementCommands.Steering.performed += ApplySteering;
        input.MovementCommands.Steering.canceled += ReleaseSteering;
        input.MovementCommands.Handbrake.performed += ApplyHandbrake;
        input.MovementCommands.Handbrake.canceled += ReleaseHandbrake;
    }

    // Inverse of OnEnable()
    private void OnDisable()
    {
        input.Disable();
    }

    // Handles Throttle functions. Takes inputs and sets them as value
    private void ApplyThrottle(InputAction.CallbackContext value)
    {
        throttleInput = value.ReadValue<float>();
    }

    // Same as previous but sets throttle to 0 when no input is detected
    private void ReleaseThrottle(InputAction.CallbackContext value)
    {
        throttleInput = 0;
    }
    
    // Handles Steering functions. Takes inputs and sets them as value
    private void ApplySteering(InputAction.CallbackContext value)
    {
        steeringInput = value.ReadValue<float>();
    }

    // Same as previous but sets steering to 0 when no input is detected
    private void ReleaseSteering(InputAction.CallbackContext value)
    {
        steeringInput = 0;
    }
    
    // Handles the Handbrake function. Takes inputs and sets them as value
    private void ApplyHandbrake(InputAction.CallbackContext value)
    {
        handBrakeInput = value.ReadValue<float>();
    }

    // Same as previous but sets handbrake to 0 when no input is detected
    private void ReleaseHandbrake(InputAction.CallbackContext value)
    {
        handBrakeInput = 0;
    }
}
