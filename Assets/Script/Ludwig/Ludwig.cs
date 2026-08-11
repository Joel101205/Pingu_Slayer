using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ludwig : MonoBehaviour
{

    public Transform player;
    private bool isFlipped = false;
    private Rigidbody2D rb;
    private Rigidbody2D rbPlayer;
    public float leftBound;
    public float rightBound;
    public float health;
    public healthbar healthbar;
    private SpriteRenderer SpriteRenderer;
    private Material originalMaterial;
    public Material flash;
    private Animator animator;
    private bool isDead;
    private BoxCollider2D collider;
    private float playerInvincible = 0;
    
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthbar.setMaxHealth(health);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = SpriteRenderer.material;
        animator = GetComponent<Animator>();
        isDead = false;
        collider = GetComponent<BoxCollider2D>();
    }

    public void Update()
    {
        FixPosition();
        if (health <= 0 && !isDead)
        {
            isDead = true;
            dying();
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Slam()
    {
        if (isFlipped)
        {
            rb.AddForce(new Vector2(5f, 1) * 1000f);
        }
        else
        {
            rb.AddForce(new Vector2(-5f, 1) * 1000f);
        }
    }

    public IEnumerator Jump()
    {
        if (isFlipped)
        {
            rb.AddForce(new Vector2(0,2) * 1000f);
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(0.01f);
                rb.AddForce(new Vector2(1, 0) * 1000f);    
            }
            
        }
        else
        {
            rb.AddForce(new Vector2(0, 2) * 1000f);
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(0.01f);
                rb.AddForce(new Vector2(-1, 0) * 1000f);    
            }
        }
    }
    
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerInvincible == 0)
        {
            StartCoroutine(playerInvinc());
            Debug.Log("Collided with Player");
            other.GetComponent<Health>().getHit();
        }
        
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collided with Ground");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
        }
    }
    
    private IEnumerator playerInvinc()
    {
        playerInvincible = 2;
        yield return new WaitForSeconds(1);
        playerInvincible--;
        yield return new WaitForSeconds(1);
        playerInvincible--;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ludwig jumped");
            rb.gravityScale = 15;
        }
    }

    public void FixPosition()
    {
        if (transform.position.y < -0.877)
        {
            transform.position = new Vector3(transform.position.x, -0.877f, 0);
        }

        if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound, transform.position.y, 0);
        }

        if (transform.position.x < leftBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, 0);
        }
    }
    
    public void takeMeeleDamage()
    {
        health -= 5;
        healthbar.setHealth(health);
        StartCoroutine(Flash());
    }

    public void takeRangeDamage()
    {
        health -= 1;
        healthbar.setHealth(health);
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        SpriteRenderer.material = flash;
        yield return new WaitForSeconds(0.15f);
        SpriteRenderer.material = originalMaterial;
    }

    public void dying()
    {
        animator.ResetTrigger("Walking");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Slam");
        animator.SetTrigger("Dying");
        animator.Play("Dying");
        collider.enabled = false;
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(transform.position.x, -0.8f);
        rb.isKinematic = true;
    }

    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
