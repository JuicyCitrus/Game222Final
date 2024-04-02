using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float lookSpeed = 1f;
    [SerializeField]
    private Transform cameraTarget;

    private PlayerInputAction pInput;
    private Quaternion desiredRotation;
    private Vector3 lookAngle,moveDirection;
    private CharacterController cController;
    public static Action ToggleCameraPerspective = delegate { };

    private void Start()
    {
        cameraTarget.parent = null;
        pInput = new PlayerInputAction();
        pInput.Enable();

        //connects the input system to the camera perspective toggle delegate
        pInput.Player.CameraToggle.performed += ToggleCamera;
        cController = GetComponent<CharacterController>();
    }
    private void OnDisable()
    {
        pInput.Player.CameraToggle.performed -= ToggleCamera;
    }
    // Update is called once per frame
    void Update()
    {
        CameraControls();
        MovementControls();       
    }
    private void CameraControls()
    {
        //Horizontal camera rotation
        desiredRotation = cameraTarget.rotation;
        desiredRotation *= Quaternion.AngleAxis(pInput.Player.Aim.ReadValue<Vector2>().x * lookSpeed, Vector3.up);

        //Vertical camera rotation
        desiredRotation *= Quaternion.AngleAxis(-pInput.Player.Aim.ReadValue<Vector2>().y * lookSpeed, Vector3.right);

        //clamp rotation
        lookAngle = desiredRotation.eulerAngles;
        lookAngle.z = 0;
        if (lookAngle.x > 180 && lookAngle.x < 340) //this is equal to -20 but negative numbers aren't compatible and equates to 360 - 20 = 340
            lookAngle.x = 340;
        else if (lookAngle.x < 180 && lookAngle.x > 40)
            lookAngle.x = 40;

        //Apply camera rotation
        cameraTarget.rotation = Quaternion.Euler(lookAngle);

        //Apply rotation to transform for playermovement
        transform.rotation = Quaternion.Euler(0, lookAngle.y, 0);
    }
    private void MovementControls()
    {
        //calculate movedirection using object orientation
        moveDirection = transform.forward * pInput.Player.Movement.ReadValue<Vector2>().y;
        moveDirection += transform.right * pInput.Player.Movement.ReadValue<Vector2>().x;
        moveDirection = moveDirection.normalized;

        //apply movemement using the character controller
        cController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
    private void ToggleCamera(InputAction.CallbackContext c)
    {
        ToggleCameraPerspective();
    }
}
