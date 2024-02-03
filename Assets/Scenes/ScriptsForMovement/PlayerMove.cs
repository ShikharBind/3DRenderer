using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb2d;
    public int health = 100;

    private void FixedUpdate()
    {
        Vector2 movementDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb2d.velocity = movementDir * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<EnemyScript>().CanMove = false;
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        health -= 5;
        Debug.Log(health);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.gameObject.GetComponent<EnemyScript>().CanMove = true;
    }
}
