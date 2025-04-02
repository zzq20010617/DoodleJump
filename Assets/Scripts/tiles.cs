using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiles : MonoBehaviour
{
    public float tileDestroyTime = 0.5f;
    public Animator anim;
    public Collider2D tileCollider;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float speed = collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;
            if (speed < 0)
            {
                Debug.Log("playanim");
                anim.SetBool("isBreak", true);
                tileCollider.enabled = false;
                StartCoroutine(DestroyTileAfterTime());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("enter below");
            tileCollider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tileCollider.enabled = true;
        }
    }

    private IEnumerator DestroyTileAfterTime()
    {
        yield return new WaitForSeconds(tileDestroyTime);
        
        Destroy(gameObject);
    }
}
