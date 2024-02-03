using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb2d;
    Vector2 movement;
    public int health = 100;

    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb2d.position = rb2d.position + movement.normalized * speed*Time.fixedDeltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        health -= 5;
        Debug.Log(health);
        // Add collision handling logic here
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered with: " + other.gameObject.name);
        // Add trigger handling logic here
    }
}
