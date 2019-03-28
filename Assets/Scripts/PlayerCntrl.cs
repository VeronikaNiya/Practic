using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{
    [SerializeField]
    private int lives = 5;
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float jumpForce = 15.0f;

    private Rigidbody2D rb;
    private Animation animator;
    private SpriteRenderer sprite;

    private bool IsGrounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animation>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0f;
    }
   
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        IsGrounded = colliders.Length > 1;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal")) Run();

        if (IsGrounded && Input.GetButtonDown("Jump")) Jump();
    }
}
