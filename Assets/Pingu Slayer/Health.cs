using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public healthbar healthbar;

    public float health;
    // Start is called before the first frame update
    void Start()
    {
        healthbar.setMaxHealth(health);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0) SceneManager.LoadScene(5);
    }

    public void getHit()
    {
        health -= 25;
        healthbar.setHealth(health);
    }

    public void hitByMissile()
    {
        health -= 10;
        healthbar.setHealth(health);
    }
}
