using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{
    [Header("monster")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] public float leftLimit = 5f;
    [SerializeField] public float rightLimit = 5f;

    public Rigidbody2D rb;
    private float xpos;
    private Vector2 direction = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        xpos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, 0);

        if (transform.position.x >= xpos + rightLimit)
        {
            direction = Vector2.left; // Move left
        }
        else if (transform.position.x <= xpos - leftLimit)
        {
            direction = Vector2.right; // Move right
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }

}
