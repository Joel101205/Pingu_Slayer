using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform player;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    
    [SerializeField] protected float speed;

    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    [SerializeField] protected int meleeDamage;
    [SerializeField] private Material damageFlash;
    [SerializeField] private Material originalMaterial;
    
    
    [SerializeField] protected float minimalDistanceToPlayer;

    protected bool isFlipped;
    protected bool isDead;

    public HealthBar healthBar;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    

    public void move()
    {
        flip();

        if (Vector2.Distance(player.position, rb.position) > minimalDistanceToPlayer)
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }

    void flip()
    {
        Vector3 flipped = rb.transform.localScale;
        flipped.z *= -1f;

        if (rb.transform.position.x > player.position.x && isFlipped)
        {
            rb.transform.localScale = flipped;
            rb.transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (rb.transform.position.x < player.position.x && !isFlipped)
        {
            rb.transform.localScale = flipped;
            rb.transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void takeMeleeDamage()
    {
        currentHealth -= meleeDamage;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(Flash());
    }
    
    private IEnumerator Flash()
    {
        spriteRenderer.material = damageFlash;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.material = originalMaterial;
    }

    public Rigidbody2D Rb
    {
        get => rb;
        set => rb = value;
    }

    public Transform Player
    {
        get => player;
        set => player = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float MinimalDistanceToPlayer
    {
        get => minimalDistanceToPlayer;
        set => minimalDistanceToPlayer = value;
    }

    public bool IsFlipped
    {
        get => isFlipped;
        set => isFlipped = value;
    }
}
