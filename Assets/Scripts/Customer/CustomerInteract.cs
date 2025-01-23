using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CustomerInteract : MonoBehaviour
{
    [SerializeField] GameObject interactObject; // Reference to the customer object
    private PlayerControls inputActions;
    private bool isPlayerInRange = false;
    private CustomerDefault currentCustomer;

    void Awake()
    {
        inputActions = new PlayerControls();
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
        if (isPlayerInRange && currentCustomer != null)
        {
            currentCustomer.PickUpCustomer(gameObject);
            Debug.Log("Customer Picked Up");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            isPlayerInRange = true;
            currentCustomer = other.GetComponent<CustomerDefault>();
            Debug.Log("Customer In Range: " + currentCustomer);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            isPlayerInRange = false;
            currentCustomer = null;
            Debug.Log("Customer Out of Range");
        }
    }
}
