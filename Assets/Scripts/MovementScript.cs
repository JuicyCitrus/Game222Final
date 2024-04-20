using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementScript : MonoBehaviour
{
    private PlayerControls pInput;
    private Vector3 moveDirection;
    private CharacterController cController;
    private Quaternion moveRotation;

    [SerializeField] private int movementSpeed = 2;
    [SerializeField] private float graviticForce = -1;

    public float xCoord;
    public float zCoord;

    private void Start()
    {
        pInput = new PlayerControls();
        pInput.Enable();
        cController = GetComponent<CharacterController>();
    }

    void Update()
    {
        xCoord = this.transform.position.x;
        zCoord = this.transform.position.z;
        MovementControls();
    }
    private void MovementControls()
    {
        //calculate movedirection using object orientation
        moveDirection = transform.forward * pInput.Player.Movement.ReadValue<Vector2>().y;
        moveDirection += transform.right * pInput.Player.Movement.ReadValue<Vector2>().x;

        if (moveDirection != Vector3.zero)
        {
            moveRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, Time.deltaTime * 10);
        }

        moveDirection.y = graviticForce;
        moveDirection = moveDirection.normalized;

        //apply movemement using the character controller
        cController.Move(moveDirection * movementSpeed * Time.deltaTime);
    }
}
