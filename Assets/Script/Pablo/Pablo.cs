using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pablo : MonoBehaviour
{
    public Transform player;
    private bool isFlipped = false;
    public bool missileAvailable;
    [SerializeField] private float MissileCooldown;
    public bool slashAvailable;
    [SerializeField] private GameObject missile;
    [SerializeField] private int NumMissile;
    [SerializeField] private healthbar healthbar;
    [SerializeField]private float health;
    private Rigidbody2D rb;
    private SpriteRenderer SpriteRenderer;
    private Material originalMaterial;
    public Material flash;
    private Animator animator;
    private bool isDead = false;
    private BoxCollider2D collider;
    private float playerInvincible = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        missileAvailable = true;
        healthbar.setMaxHealth(health);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = SpriteRenderer.material;
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        slashAvailable = !missileAvailable;
        if (health <= 0)
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
        if (other.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
        }

        if (other.gameObject.CompareTag("Player") && playerInvincible == 0)
        {
            Debug.Log("Hit by Pablo");
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

    public IEnumerator MissileCoolDown()
    {
        missileAvailable = false;
        
            
        yield return new WaitForSeconds(MissileCooldown);
        
        missileAvailable = true;
    }

    public void StartMissileCooldown()
    {
        StartCoroutine(MissileCoolDown());
    }

    public void slash()
    {
        rb.MovePosition(isFlipped ? new Vector2(rb.position.x + 1.5f,rb.position.y): new Vector2(rb.position.x-1.5f,rb.position.y));
    }

    public IEnumerator missileAttack()
    {
        for (int i = 0; i < NumMissile; i++)
        {
            Instantiate(missile, new Vector3(player.position.x, 20, 0), Quaternion.Euler(180, 0, 0));
            yield return new WaitForSeconds(1f);
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

    public void dying()
    {
        animator.ResetTrigger("Slash");
        animator.ResetTrigger("Button");
        animator.Play("Dying");
        rb.isKinematic = true;
        collider.enabled = false;
        rb.velocity = Vector2.zero;
    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private IEnumerator Flash()
    {
        SpriteRenderer.material = flash;
        yield return new WaitForSeconds(0.15f);
        SpriteRenderer.material = originalMaterial;
    }
}
