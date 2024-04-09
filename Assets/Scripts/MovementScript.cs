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
    [SerializeField] private int movementSpeed = 2;
    [SerializeField] private float graviticForce = -1;

    private void Start()
    {
        pInput = new PlayerControls();
        pInput.Enable();
        cController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovementControls();
    }
    private void MovementControls()
    {
        //calculate movedirection using object orientation
        moveDirection = transform.forward * pInput.Player.Movement.ReadValue<Vector2>().y;
        moveDirection += transform.right * pInput.Player.Movement.ReadValue<Vector2>().x;
        moveDirection.y = graviticForce;
        moveDirection = moveDirection.normalized;
        //apply movemement using the character controller
        cController.Move(moveDirection * movementSpeed * Time.deltaTime);
    }

}

