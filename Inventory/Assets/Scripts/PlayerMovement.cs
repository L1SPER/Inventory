using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float zSpeed;
    private float xSpeed;

    [SerializeField]
    private float walkingSpeedMultiplier;
    [SerializeField]
    private float runningSpeedMultiplier;
    [SerializeField]
    private float crouchSpeedMultiplier;
    [SerializeField]
    private float speedMultiplier;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private LayerMask groundLayer;
    private bool isGrounded;
    private Vector3 moveSpeed;
    [SerializeField] private Transform face;

    private bool canRun;
    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = walkingSpeedMultiplier;
        canRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Movement();
    }

    private void Movement()
    {
        xSpeed = Input.GetAxis("Vertical");
        zSpeed = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
            speedMultiplier = crouchSpeedMultiplier;
            canRun = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, 1, 1);
            speedMultiplier = walkingSpeedMultiplier;
            canRun = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && canRun)
        {
            speedMultiplier = runningSpeedMultiplier;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMultiplier = walkingSpeedMultiplier;
        }

        moveSpeed = face.forward * xSpeed * speedMultiplier + face.right * zSpeed * speedMultiplier;
        transform.position += moveSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.transform.position,groundCheckRadius);
    }
    private bool GroundCheck()
    {
        return isGrounded=Physics.CheckSphere(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }
}
