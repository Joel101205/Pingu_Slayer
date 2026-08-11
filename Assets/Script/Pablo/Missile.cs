using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Vector2 target;

    private Rigidbody2D rb;

    private Animator animator;
    [SerializeField] private float speed;
    private float playerInvincible = 0;

    private CapsuleCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fly()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, new Vector2(target.x, -3.6f), speed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    private void FixedUpdate()
    {
        fly();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerInvincible == 0)
        {
            StartCoroutine(playerInvinc());
            collision.GetComponent<Health>().hitByMissile();
            animator.SetTrigger("Explode");
        }

        if (collision.CompareTag("Wall") || collision.CompareTag("Ground"))
        {
            animator.SetTrigger("Explode");
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

    public void explode()
    {
        Destroy(gameObject);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");    
        }
        
    }

    public void disable()
    {
        collider.enabled = false;
    }
}

