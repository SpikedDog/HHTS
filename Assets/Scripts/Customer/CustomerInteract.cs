using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CustomerInteract : MonoBehaviour
{
    private Collider interactionCollider;
    private PlayerControls inputActions;
    private bool isPlayerInRange = false;

    void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void Update()
    {
        if (!interactionCollider)
        {
            interactionCollider = transform.Find("Sphere").GetComponentInChildren<Collider>();
        }
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
            Debug.Log("OnClick Active");
        }
    }

    void OnTriggerEnter(Collider other)
    {
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


    //private void OnTriggerEnter(Collider player)
    //{
    //    if (player.CompareTag("Player"))
    //    {

    //    }
    //}
}
