using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gustav : MonoBehaviour
{
    public Transform player;
    private bool isFlipped = false;
    [SerializeField] private float leftBound;
    [SerializeField]private float rightBound;
    public healthbar healthbar;
    public float health;
    private SpriteRenderer SpriteRenderer;
    private Material originalMaterial;
    public Material flash;
    private Animator animator;
    private bool isDead = false;
    private BoxCollider2D collider;
    private float playerInvincible = 0;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthbar.setMaxHealth(health);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = SpriteRenderer.material;
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        wallStop();
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collided with Ground");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
        }

        if (other.gameObject.CompareTag("Player") && playerInvincible == 0)
        {
            Debug.Log("Collided with Player");
            StartCoroutine(playerInvinc());
            other.GetComponent<Health>().getHit();
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

    public void Slash()
    {
        if (isFlipped)
        {
            rb.MovePosition(new Vector2(transform.position.x+2,transform.position.y));
        }
        else
        {
            rb.MovePosition(new Vector2(transform.position.x-2, transform.position.y));
        }
    }

    public void Thrust()
    {
        if (isFlipped)
        {
            rb.MovePosition(new Vector2(transform.position.x+7, transform.position.y));
        }
        else
        {
            rb.MovePosition(new Vector2(transform.position.x-7, transform.position.y));
        }
    }

    public void wallStop()
    {
        if (transform.position.x < leftBound)
        {
            transform.position = new Vector2(leftBound, transform.position.y);
        }

        if (transform.position.x > rightBound)
        {
            transform.position = new Vector2(rightBound, transform.position.y);
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

    private void dying()
    {
        animator.ResetTrigger("Thrust");
        animator.ResetTrigger("Slash");
        animator.SetTrigger("Dying");
        animator.Play("Dying");
        collider.enabled = false;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
