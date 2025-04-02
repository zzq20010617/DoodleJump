using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    public float speed = 8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    public void shootBullet(Vector2 direction)
    {
        rb.velocity = direction * speed;

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            Destroy(collider.gameObject);
        }
    }
}
