using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float xRotationSpeed;
    [SerializeField] float yRotationSpeed;
    private float horizontalInput;
    private float verticalInput;

    private float verticalAngle;
    private float horizontalAngle;
    [SerializeField] Transform playerBody;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        horizontalInput = Input.GetAxis("Mouse X")*Time.deltaTime* xRotationSpeed;
        verticalInput= Input.GetAxis("Mouse Y")*Time.deltaTime* yRotationSpeed;

        playerBody.Rotate(Vector3.up * horizontalInput);

        verticalAngle -= verticalInput;
        horizontalAngle += horizontalInput;

        verticalAngle = Mathf.Clamp(verticalAngle, -30f, 90f);

        transform.rotation=Quaternion.Euler(verticalAngle, horizontalAngle, 0f);
    }
}
