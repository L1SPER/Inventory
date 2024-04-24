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
    private float speedMultiplier;
    private Rigidbody rb;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private LayerMask groundLayer;
    private bool isGrounded;
    private Vector3 moveSpeed;
    [SerializeField] private Transform face;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

        moveSpeed = face.forward * xSpeed* speedMultiplier + face.right * zSpeed* speedMultiplier;
        transform.position += moveSpeed;

        //rb.velocity= new Vector3(xSpeed* speedMultiplier*Time.deltaTime, 0f, zSpeed* speedMultiplier*Time.deltaTime);
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
