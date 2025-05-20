using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 8f;
    private float horizontal;

    [Header("Jumping")]
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private bool canDoubleJump;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private float dashCooldownTimer;
    private bool isDashing;

    [Header("Gravity")]
    [SerializeField] private float normalGravity = 3f;
    [SerializeField] private float increasedGravity = 10f;
    private bool isPressingS;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    private bool isFacingRight = true;
    public bool isWalking;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
            canDoubleJump = true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            coyoteTimeCounter = 0f;

            // Wibracja przy skoku
            VibrationManager.Instance.Vibrate(0.3f, 0.3f, 0.15f);
        }
        else if (Input.GetButtonDown("Jump") && !grounded && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            canDoubleJump = false;

            // Wibracja przy double jumpie
            VibrationManager.Instance.Vibrate(0.35f, 0.35f, 0.15f);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Dash"))
            && dashCooldownTimer <= 0f)
        {
            StartCoroutine(Dash());
        }
        dashCooldownTimer = Mathf.Max(0f, dashCooldownTimer - Time.deltaTime);

        isPressingS = Input.GetKey(KeyCode.S)
                      || Input.GetButton("SlamDown")
                      || Input.GetAxisRaw("Vertical") < -0.5f;

        if (!grounded && isPressingS && rb.velocity.y < -1f)
        {
            // Slam-down wibracja (jednorazowo, jeœli chcesz j¹ ograniczyæ raz na wejœcie — daj mi znaæ)
            VibrationManager.Instance.Vibrate(0.5f, 0.5f, 0.1f);
        }

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 s = transform.localScale;
            s.x *= -1f;
            transform.localScale = s;
        }

        isWalking = Mathf.Abs(horizontal) > 0.1f && grounded && !isDashing;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", grounded);
        animator.SetBool("isJumping", !grounded);
        animator.SetBool("isDashing", isDashing);
    }

    private void FixedUpdate()
    {
        rb.gravityScale = isPressingS ? increasedGravity : normalGravity;

        if (!isDashing)
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;
        animator.SetBool("isDashing", true);

        float origG = rb.gravityScale;
        rb.gravityScale = 0f;
        float dir = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(dir * dashSpeed, 0f);

        // Dash wibracja
        VibrationManager.Instance.Vibrate(0.7f, 0.7f, 0.2f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = origG;
        isDashing = false;
        animator.SetBool("isDashing", false);
    }
}
