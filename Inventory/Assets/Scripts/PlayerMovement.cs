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
    private bool isRunning;

    [Header("Stamina Main Parameters")]
    [SerializeField] private float currentStamina;
    [SerializeField] private float maxStamina = 100f;

    [Header("Stamina Regen Parameters")]
    [Range(0, 50)][SerializeField] private float staminaDrain=20;
    [Range(0, 50)][SerializeField] private float staminaRegen=5;

    public StaminaBar staminaBar;

    void Start()
    {
        canRun = true;
        isRunning= false;
        speedMultiplier = walkingSpeedMultiplier;
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }
    void Update()
    {
        StaminaControl();
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
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || !canRun)
        {
            speedMultiplier = walkingSpeedMultiplier;
            isRunning = false;
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
    private void StaminaControl()
    {
        if (!isRunning)
            RegenerateStamina();
        else if(isRunning)
            DrainStamina();

        if(currentStamina<=0)
        {
            canRun = false;
            currentStamina = 0;
        }
        else
            canRun = true;
    }
    public void DrainStamina()
    {
        currentStamina -= staminaDrain * Time.deltaTime;
        staminaBar.SetStamina(currentStamina);
    }
    private void RegenerateStamina()
    {
        if (currentStamina <= maxStamina - 0.01)
        {
            currentStamina += staminaRegen * Time.deltaTime;
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }
            staminaBar.SetStamina(currentStamina);
        }
    }
}
