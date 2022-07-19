using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public float speed = 5.0f;
    public int jumpForce = 300;
    public bool isGrounded;
    public LayerMask isGroundlayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.02f;
    public bool isJumpAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 525;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        bool isAttack = Input.GetButtonDown("Fire1");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundlayer);
        //isJumpAttack = !isGrounded && rb.velocity.y < 0 && Input.GetKeyDown("w");

        if (!isGrounded && rb.velocity.y < 0 && Input.GetKeyDown(KeyCode.W))
        {
            isJumpAttack = true;
        }

        if (isGrounded)
        {
            isJumpAttack = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }
        
        if (isJumpAttack)
        {
            rb.gravityScale = 3;
        }
        else
        {
            rb.gravityScale = 1;
        }

        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("MoveValue", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumpAttack", isJumpAttack);
        anim.SetBool("isAttack", isAttack);

        //Sprite flipping
        if (hInput < 0)
        {
            sr.flipX = true;
        }
        else if (hInput > 0)
        {
            sr.flipX = false;
        }
    }
}