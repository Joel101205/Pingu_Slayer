using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    private Vector2 moveInput;
    
    [SerializeField] private float groundCheckMargin;
    [SerializeField] private LayerMask ground;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Jump()
    {
        if (GroundCheck())
        {
            Debug.Log("Jumped");
            rb.AddForce(new Vector2(0.0f, jumpForce));    
        }
        
    }

    public void Movement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(moveInput.x * movementSpeed, rb.linearVelocity.y);
    }

    private bool GroundCheck()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckMargin, ground);
    }


    private void Start()
    {
        groundCheckMargin += spriteRenderer.bounds.extents.y;
    }
}

