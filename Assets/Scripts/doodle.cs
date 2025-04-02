using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class doodle : MonoBehaviour
{
    public static PlayerInput PlayerInput;


    [Header("doodle")]
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float gainUpSpeed = 10f;
    [SerializeField] public Rigidbody2D rb;
    public int facingDirection = -1;
    public Animator anim;
    public Bullet bulletPrefab;
    private Bullet bullet;
    public Transform mouth;
    public float cooldown = 2;
    private float timer;

    private InputAction mousePositionAction;

    public static Vector2 MousePosition;
    private InputAction mouseAction;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        mousePositionAction = PlayerInput.actions["MousePosition"];
        mouseAction = PlayerInput.actions["Mouse"];

    }

    // Update is called once per frame

    void Update()
    {
        MousePosition = mousePositionAction.ReadValue<Vector2>();
        if (mouseAction.WasPressedThisFrame())
        {
            attack();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        int horizontal = Keyboard.current.aKey.isPressed ? -1 :
             Keyboard.current.dKey.isPressed ? 1 : 0;

        if (horizontal > 0 && transform.localScale.x < 0 ||
            horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
/*        anim.SetFloat("horizontal", Mathf.Abs(horizontal));*/

        rb.velocity = new Vector2(horizontal * horizontalSpeed, rb.velocity.y);

    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
            transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f) // Only detect collision from below
            {
                rb.velocity = new Vector2(rb.velocity.x, gainUpSpeed);
                break;
            }
        }
    }

    private void attack()
    {
        if (timer <= 0)
        {
            Vector2 spawnPos = mouth.position;
            bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(MousePosition);
            Vector2 direction = (mouseWorldPos - (Vector2)bullet.transform.position).normalized;
            bullet.shootBullet(direction);
            timer = cooldown;
        }
    }
}
