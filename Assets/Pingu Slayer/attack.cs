using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    private Animator animator;

    public Transform attackPoint;

    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    public float cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown == 0)
        {
            StartCoroutine(attacking());
        }
    }

    public IEnumerator attacking()
    {
        cooldown = 1;
        animator.Play("Attack");
        Collider2D[] boss = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach (Collider2D enemy in boss)
        {
            switch (enemy.gameObject.name)
            {
                case "Ludwig":
                    enemy.GetComponent<Ludwig>().takeMeeleDamage();
                    break;
                case "Gustav":
                    enemy.GetComponent<Gustav>().takeMeeleDamage();
                    break;
                case "Pablo":
                    enemy.GetComponent<Pablo>().takeMeeleDamage();
                    break;
            }
            
        }

        yield return new WaitForSeconds(0.25f);
        cooldown = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
