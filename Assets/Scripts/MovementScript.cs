using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementScript : MonoBehaviour
{
    [SerializeField]
    private int movementSpeed = 2;

    [SerializeField]
    private int rotationSpeed = 10;

    [SerializeField]
    private float graviticForce = -1;

    [SerializeField]
    private GameObject target;

    private PlayerControls pInput;
    private CharacterController cController;

    public float xCoord;
    public float yCoord;
    public float zCoord;

    private void Start()
    {
        pInput = new PlayerControls();
        pInput.Enable();
        cController = GetComponent<CharacterController>();
    }

    void Update()
    {
        xCoord = target.transform.position.x;
        yCoord = target.transform.position.y;
        zCoord = target.transform.position.z;
        MovementControls();
    }
    private void MovementControls()
    {
        Vector2 movementInput = pInput.Player.Movement.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);

        if (moveDirection != Vector3.zero)
        {
            float Angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
            Quaternion moveRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, Time.deltaTime * rotationSpeed);
        }

        moveDirection.y = graviticForce;

        cController.Move(moveDirection * movementSpeed * Time.deltaTime);
    }

        // --- Version 3 ---

        /*// Get movement info from player input
        Vector2 movementInput = pInput.Player.Movement.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);

        // Get rotation info from player input
        Vector2 rotateInput = pInput.Player.Rotate.ReadValue<Vector2>();
        Vector3 rotateDirection = new Vector3(rotateInput.x, 0f, rotateInput.y);

        // Perform rotation
        float Angle = Mathf.Atan2(rotateDirection.x, rotateDirection.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
        Quaternion moveRotation = Quaternion.Euler(0f, Angle, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, Time.deltaTime * rotationSpeed);
       
        // Apply gravity
        moveDirection.y = graviticForce;

        // Perform movement
        cController.Move(moveDirection * movementSpeed * Time.deltaTime);*/


        // --- Version 2 --- 

        /* Vector2 movementInput = pInput.Player.Movement.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;

        if (moveDirection != Vector3.zero)
        {
            moveRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, Time.deltaTime * 10);
        }

        moveDirection.y = graviticForce;

        cController.Move(moveDirection * movementSpeed * Time.deltaTime); */


        // --- Version 1 ---
        /* moveDirection = transform.forward * pInput.Player.Movement.ReadValue<Vector2>().y;
        moveDirection += transform.right * pInput.Player.Movement.ReadValue<Vector2>().x;

        if (moveDirection != Vector3.zero)
        {
            moveRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, Time.deltaTime * 10);
        }

        moveDirection.y = graviticForce;
        moveDirection = moveDirection.normalized;

        cController.Move(moveDirection * movementSpeed * Time.deltaTime); */
}
