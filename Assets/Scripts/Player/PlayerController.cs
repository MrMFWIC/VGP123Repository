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
    public int jumpForce = 375;
    public bool isGrounded;
    public LayerMask isGroundlayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.02f;
    public bool isJumpAttack;

    Coroutine jumpForceChange;

    private int _lives = 3;
    public int maxLives = 5;

    public int lives
    {
        get { return _lives; }
        set 
        {
            /*if (_lives > value)
            {
                Lost a life - Respawn
            }*/

            _lives = value;

            if (_lives > maxLives)
            {
                _lives = maxLives;
            }

            /*if (_lives < 0)
            {
                Game Over
            }*/

            Debug.Log("Lives are set to: " + lives.ToString());
        }
    }

    private int _score = 0;

    public int score
    {
        get { return _score; }
        set
        {
            _score = value;

            Debug.Log("Your current score is: " + score.ToString());
        }
    }

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
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundlayer);

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
        
        if (isGrounded)
        {
            rb.gravityScale = 1;
        }

        anim.SetFloat("MoveValue", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumpAttack", isJumpAttack);

        if (hInput != 0)
        {
            sr.flipX = (hInput < 0);
        }

        if (curPlayingClip.Length > 0)
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "Attack")
                anim.SetTrigger("Attack");
            else if (curPlayingClip[0].clip.name == "Attack")
                rb.velocity = Vector2.zero;
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }
    }
    
    public void IncreaseGravity()
        {
            rb.gravityScale = 3;
        }

    public void StartJumpForceChange()
    {
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
    }

    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;

        yield return new WaitForSeconds(8.0f);

        jumpForce /= 2;
        jumpForceChange = null;
    }
}