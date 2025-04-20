using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private float dashCooldownTimer = 0f;
    private bool isDashing = false;

    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter = 0f;

    // Mechanika zwiêkszonej grawitacji
    [SerializeField] private float increasedGravity = 10f;
    [SerializeField] private float normalGravity = 3f;
    private bool isPressingS = false;

    public bool isWalking = false;
    [SerializeField] private Animator animator;

    private bool canDoubleJump = false;

    void Update()
    {
        // Wejœcie poziome
        horizontal = Input.GetAxisRaw("Horizontal");

        // Coyote time i reset podwójnego skoku
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            canDoubleJump = true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Skok i podwójny skok
        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetTrigger("Jump");
            coyoteTimeCounter = 0f;
            canDoubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && !IsGrounded() && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetTrigger("Jump");
            canDoubleJump = false;
        }

        // Skracanie skoku przy puszczeniu przycisku
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f)
        {
            StartCoroutine(Dash());
        }
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        // Sprawdzanie przytrzymania S dla zwiêkszonej grawitacji
        isPressingS = Input.GetKey(KeyCode.S);

        // Ustawianie parametrów Animatora
        bool grounded = IsGrounded();
        animator.SetBool("isGrounded", grounded);
        animator.SetBool("isFalling", rb.velocity.y < 0f && !grounded);

        // Flip i chodzenie
        Flip();
        isWalking = Mathf.Abs(horizontal) > 0.1f && grounded && !isDashing;
        animator.SetBool("isWalking", isWalking);
    }

    void FixedUpdate()
    {
        // Grawitacja podczas przytrzymania S
        rb.gravityScale = isPressingS ? increasedGravity : normalGravity;

        // Ruch poziomy, pomijaj¹c dash
        if (!isDashing)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        float dashDirection = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }
}
