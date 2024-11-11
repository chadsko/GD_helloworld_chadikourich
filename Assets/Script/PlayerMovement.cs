using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private Rigidbody rb;
    private bool isGrounded;
    private float groundCheckDistance = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * horizontal + transform.forward * vertical).normalized;

        float currentMoveSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sprintSpeed : moveSpeed;

        rb.velocity = new Vector3(moveDirection.x * currentMoveSpeed, rb.velocity.y, moveDirection.z * currentMoveSpeed);

        if (isGrounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, -2f, rb.velocity.z);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * -2f * gravity), rb.velocity.z);
        }

        rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

        float turn = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * turn * rotationSpeed);
    }
}


