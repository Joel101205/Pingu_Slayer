using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 velocity;

    private float HorizontalInput;
    private float VerticalInput;
    
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rbLuwig;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
    }

    public void FixedUpdate()
    {
        run();
        jump();
    }

    public void getInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxisRaw("Jump");
    }

    public void run()
    {
        velocity.x = HorizontalInput * runSpeed;
        _rigidbody2D.velocity = velocity;
    }

    public void jump()
    {
        velocity.y = VerticalInput * jumpForce;
        _rigidbody2D.velocity = velocity;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Collided with boss");
        }
    }
}
