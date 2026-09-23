using UnityEngine;

public class Gustav : Boss
{
    [SerializeField] private float thrustDistance;
    [SerializeField] private float slashDistance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Thrust()
    {
        if (isFlipped)
        {
            rb.MovePosition(Vector2.right * thrustDistance + new Vector2(transform.position.x, transform.position.y));
        }
        else
        {
            rb.MovePosition(Vector2.left * thrustDistance + new Vector2(transform.position.x, transform.position.y));
        }
    }

    void Slash()
    {
        if (isFlipped)
        {
            rb.MovePosition(Vector2.right * slashDistance + new Vector2(transform.position.x, transform.position.y));
        }
        else
        {
            rb.MovePosition(Vector2.left * slashDistance + new Vector2(transform.position.x, transform.position.y));
        }
    }
}
