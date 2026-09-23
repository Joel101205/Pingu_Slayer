using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Ludwig : Boss
{
    [SerializeField] private float slamForce;
    [SerializeField] private float slamDistance;

    [SerializeField] private float jumpForce;

    [SerializeField] private float jumpDistance;

    void Update()
    {
        if (!isDead)
        {
            dying();    
        }
        
    }
    
    public void Slam()
    {
        if (isFlipped)
        {
            rb.AddForce(Vector2.up * slamForce);
            rb.MovePosition(slamDistance * Vector2.right + new Vector2(transform.position.x, transform.position.y));
        }
        else
        {
            rb.AddForce(Vector2.up * slamForce);
            rb.MovePosition(slamDistance * Vector2.left + new Vector2(transform.position.x, transform.position.y));
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
        if (isFlipped)
        {
            for (int i = 0; i < 10; i++)
            {
                rb.AddForce(Vector2.right * jumpDistance * 2);    
            }
            
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                rb.AddForce(Vector2.left * jumpDistance * 2);    
            }  
        }
    }

    public void inAir()
    {
        if (isFlipped)
        {
            for (int i = 0; i < 5; i++)
            {
                rb.AddForce(Vector2.right * jumpDistance);    
            }
            
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                rb.AddForce(Vector2.left * jumpDistance);    
            }
        }
    }

    public void dying()
    {
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Dying");
            isDead = true;
            animator.ResetTrigger("Walking");
            animator.ResetTrigger("Slam");
            animator.ResetTrigger("Jump");

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("inAir") && other.transform.CompareTag("Ground"))
        {
            animator.Play("landing");   
        }
    }
}
