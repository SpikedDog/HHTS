using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CustomerInteract : MonoBehaviour
{
    public Collider interactionCollider;
    private PlayerControls inputActions;
    private bool isPlayerInRange = false;

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
        if (isPlayerInRange)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other == interactionCollider)
        {
            isPlayerInRange = true;
            Debug.Log("Delete True");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other == interactionCollider)
        {
            isPlayerInRange = false;
            Debug.Log("Delete False");
        }
    }


    //private void OnTriggerEnter(Collider player)
    //{
    //    if (player.CompareTag("Player"))
    //    {

    //    }
    //}
}
