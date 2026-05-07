using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CustomerInteract : MonoBehaviour
{
    [SerializeField] GameObject interactObject;
    private PlayerControls inputActions;
    private bool isPlayerInRange = false;
    public CustomerDefault customerDefault;
    public bool customerActive;
    [SerializeField] MyCarSound myCarSound;

    void Awake()
    {
        inputActions = new PlayerControls();
        myCarSound = FindObjectOfType<MyCarSound>();
        //customerDefault = interactObject.GetComponent<CustomerDefault>();
    }

    void OnEnable()
    {
        inputActions.MovementCommands.Interaction.performed += OnClick;
        inputActions.MovementCommands.Enable();
        //Debug.Log("Interaction Enabled");
    }

    void OnDisable()
    {
        inputActions.MovementCommands.Interaction.performed -= OnClick;
        inputActions.MovementCommands.Disable();
        //Debug.Log("Interaction Disabled");
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        //Debug.Log("Temp1");
        if (isPlayerInRange && customerDefault != null)
        {
            //Debug.Log("OnClick Active");
            myCarSound.PickUpSound();
            customerDefault.Interact();
        }
    }

    



    public void SetHector(CustomerDefault aHector)
    {
        customerDefault = aHector;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Temp2");
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            //Debug.Log("Delete True");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            //Debug.Log("Delete False");
        }
    }
}