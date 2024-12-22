using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Snake snake;

    void Awake()
    {
        health = maxHealth;
        Debug.Log(gameObject.name+"'s health set at "+health);
    }
    
    // Fungsi mengurangi health
    public void Subtract(int value) {
        health -= value;
        Debug.Log(gameObject.name + " took "+value+" damage.");
        if(health <= 0) {
            Destroy(gameObject);
            if(snake) {
                snake.Die();
            }
        }
    }
}
