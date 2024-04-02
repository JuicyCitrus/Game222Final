using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveToggle : MonoBehaviour
{
    [SerializeField]
    private float thirdPersonCameraDistance;
    [SerializeField]
    private CinemachineVirtualCamera vCam;

    private bool bThirdPerson;   

    private void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        PlayerMovement.ToggleCameraPerspective += ToggleCamera;
    }
    private void OnDisable()
    {
        PlayerMovement.ToggleCameraPerspective -= ToggleCamera;
    }
    private void ToggleCamera()
    {
        if(bThirdPerson)
        {
            bThirdPerson = false;
            vCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = 0;
        }
        else
        {
            bThirdPerson = true;
            vCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = thirdPersonCameraDistance;
        }
    }
}
