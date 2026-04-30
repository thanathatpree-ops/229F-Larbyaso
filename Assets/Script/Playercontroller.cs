using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float jumpForce = 10f;

    [Header("Jump Settings")]
    public int maxJumps = 2;
    private int jumpsRemaining;
    public float fallMultiplier = 2.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
        

        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        if (isGrounded && rb.linearVelocity.y <= 0.1f)
        {
            jumpsRemaining = maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsRemaining > 0)
            {
                Jump();
            }
        }

        if (horizontalInput > 0 && !facingRight) Flip();
        else if (horizontalInput < 0 && facingRight) Flip();

        
        float moveInput = Input.GetAxisRaw("Horizontal");

        
        if (moveInput != 0)
        {
            anim.SetBool("isRunning", true); 
        }
        else
        {
            anim.SetBool("isRunning", false); 
        }

        anim.SetBool("isGround", isGrounded);

        if (rb.linearVelocity.y < 0)
        {
            
            rb.gravityScale = fallMultiplier;
        }
        else
        {
            
            rb.gravityScale = 1f;
        }
    }

    void FixedUpdate()
    {
        
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        
        anim.SetTrigger("Jump");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        jumpsRemaining--;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}