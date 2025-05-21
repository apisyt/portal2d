using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float slamForce = 25f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private float dashCooldownTimer = 0f;
    private bool isDashing = false;

    [Header("Gravity Settings")]
    [SerializeField] private float increasedGravity = 10f;
    [SerializeField] private float normalGravity = 3f;
    private bool isPressingS = false;

    [Header("Jump Settings")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter = 0f;
    private bool canDoubleJump = false;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    public bool isWalking = false;
    private bool isSlamming = false;

    void Update()
    {
        // 1) Wejœcie poziome
        horizontal = Input.GetAxisRaw("Horizontal");

        // 2) Sprawdzenie, czy na ziemi
        bool grounded = IsGrounded();

        // 3) Coyote time & reset double jump
        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
            canDoubleJump = true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // 4) Skok
        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            coyoteTimeCounter = 0f;
            canDoubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && !grounded && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            canDoubleJump = false;
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // 5) Dash
        if (Input.GetButtonDown("Dash") && dashCooldownTimer <= 0f && !isDashing && !isSlamming)
        {
            StartCoroutine(Dash());
        }
        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;

        // 6) SlamDown (kwadrat/S)
        if (Input.GetButtonDown("SlamDown") && !grounded && !isDashing && !isSlamming)
        {
            StartCoroutine(SlamDown());
        }

        // 7) Zwiêkszona grawitacja (klawisz S)
        isPressingS = Input.GetKey(KeyCode.S);

        // 8) Flip sprite
        Flip();

        // 9) Ustawienia animatora
        isWalking = Mathf.Abs(horizontal) > 0.1f && grounded && !isDashing && !isSlamming;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", grounded);
        animator.SetBool("isJumping", !grounded && !isSlamming);
        animator.SetBool("isDashing", isDashing);
        animator.SetBool("isSlamming", isSlamming);
    }

    void FixedUpdate()
    {
        // Zmiana grawitacji
        rb.gravityScale = isPressingS ? increasedGravity : normalGravity;

        // Ruch poziomy (wy³¹czony podczas dashu/slamu)
        if (!isDashing && !isSlamming)
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
            Vector3 s = transform.localScale;
            s.x *= -1f;
            transform.localScale = s;
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;
        float originalGravity = rb.gravityScale;

        animator.SetBool("isDashing", true);
        rb.gravityScale = 0f;
        float dir = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(dir * dashSpeed, 0f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", false);
    }

    private IEnumerator SlamDown()
    {
        isSlamming = true;
        animator.SetBool("isSlamming", true);

        // Nadaj prêdkoœæ w dó³
        rb.velocity = new Vector2(rb.velocity.x, -slamForce);

        // Czekaj a¿ wróci na ziemiê
        yield return new WaitUntil(() => IsGrounded());

        // Krótkie opóŸnienie, ¿eby animacja zd¹¿y³a siê zagraæ
        yield return new WaitForSeconds(0.1f);

        isSlamming = false;
        animator.SetBool("isSlamming", false);
    }
}
