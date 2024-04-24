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

    public Camera fpsCam;
    [SerializeField] 
    private float range;

    
    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = walkingSpeedMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Movement();
        Shoot();
    }

    private void Movement()
    {
        xSpeed = Input.GetAxis("Vertical");
        zSpeed = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMultiplier = runningSpeedMultiplier;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMultiplier = walkingSpeedMultiplier;
        }

        moveSpeed = face.forward * xSpeed* speedMultiplier + face.right * zSpeed* speedMultiplier;
        transform.position += moveSpeed;
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            ItemInfo itemInfo = hit.transform.GetComponent<ItemInfo>();
            if(itemInfo != null)
            {
                itemInfo.openCanvas = true;
                itemInfo.OpenCanvas();
                if(Input.GetButtonDown("Fire1"))
                {
                    //Envantere alýncak.
                }

            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.transform.position,groundCheckRadius);
        Gizmos.DrawRay(fpsCam.transform.position, fpsCam.transform.forward);
    }
    private bool GroundCheck()
    {
        return isGrounded=Physics.CheckSphere(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }
}
