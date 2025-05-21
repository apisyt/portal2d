using UnityEngine;

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

    [SerializeField] private float increasedGravity = 10f;
    [SerializeField] private float normalGravity = 3f;
    private bool isPressingS = false;
    public bool isWalking = false;

    [SerializeField] private Animator animator;

    private bool canDoubleJump = false;

    void Update()
    {
        // Pobranie wejœcia poziomego
        horizontal = Input.GetAxisRaw("Horizontal");

        bool grounded = IsGrounded();

        // Coyote Time i reset podwójnego skoku
        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
            canDoubleJump = true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Skok z ziemi
        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            coyoteTimeCounter = 0f;
            canDoubleJump = true;
        }
        // Double Jump
        else if (Input.GetButtonDown("Jump") && !grounded && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
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

        // Zwiêkszona grawitacja przy przytrzymaniu 'S'
        isPressingS = Input.GetKey(KeyCode.S);

        // Odwracanie kierunku sprite'a
        Flip();

        // Ustawianie parametrów Animatora
        isWalking = Mathf.Abs(horizontal) > 0.1f && grounded && !isDashing;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", grounded);
        animator.SetBool("isJumping", !grounded);
        animator.SetBool("isDashing", isDashing);
    }

    void FixedUpdate()
    {
        // Zmiana grawitacji
        rb.gravityScale = isPressingS ? increasedGravity : normalGravity;

        // Ruch poziomy (wy³¹czony podczas dashu)
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
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        // Start animacji dash
        animator.SetBool("isDashing", true);

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        float dashDirection = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0f);

        yield return new WaitForSeconds(dashDuration);

        // Przywrócenie stanu po dashu
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", false);
    }
}
