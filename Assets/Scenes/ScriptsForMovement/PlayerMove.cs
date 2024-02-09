using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb2d;
    public int health = 100;
    [SerializeField] private Transform FOVTransform;

    private void Update()
    {
        UpdateFOV();
    }

    private void FixedUpdate()
    {
        Vector2 movementDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb2d.velocity = movementDir * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().CanMove = false;
            Debug.Log("Collision detected with: " + collision.gameObject.name);
            health -= 5;
            Debug.Log(health);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyScript>().CanMove = true;
        }
    }

    private void UpdateFOV()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (mousePosition - transform.position).normalized;
        // FOVTransform.forward = dir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        FOVTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
