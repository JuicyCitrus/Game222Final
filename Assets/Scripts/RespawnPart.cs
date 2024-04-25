using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPart : MonoBehaviour
{
    private Vector3 SpawnPoint;
    private Quaternion SpawnRotation;

    private void Awake()
    {
        SpawnPoint = this.transform.position;
        SpawnRotation = this.transform.rotation;
    }

    private void Respawn()
    {
        this.transform.position = SpawnPoint;
        this.transform.rotation = SpawnRotation;
    }

    private void OnEnable()
    {
        ResetButton.ResetScene += Respawn;
    }

    private void OnDisable()
    {
        ResetButton.ResetScene -= Respawn;
    }
}
