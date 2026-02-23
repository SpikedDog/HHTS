using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CustomerInteract : MonoBehaviour
{
    [SerializeField] GameObject interactObject;
    private PlayerControls inputActions;
    private bool isPlayerInRange = false;
    private CustomerDefault customerDefault;
    public bool customerActive;

    void Awake()
    {
        inputActions = new PlayerControls();
        customerDefault = interactObject.GetComponent<CustomerDefault>();
    }

    void OnEnable()
    {
        inputActions.MovementCommands.Interaction.performed += OnClick;
        inputActions.MovementCommands.Enable();
        Debug.Log("Interaction Enabled");
    }

    void OnDisable()
    {
        inputActions.MovementCommands.Interaction.performed -= OnClick;
        inputActions.MovementCommands.Disable();
        Debug.Log("Interaction Disabled");
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("Temp1");
        if (isPlayerInRange && customerDefault != null)
        {
            Debug.Log("OnClick Active");
            customerDefault.Interact();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Temp2");
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Delete True");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Delete False");
        }
    }
}