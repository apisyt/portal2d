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

    private int jumpCount = 0;       // Licznik skoków
    [SerializeField] private int maxJumps = 2;  // Maksymalna liczba skoków
    private bool isGrounded;         // Czy gracz dotyka ziemi

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Sprawdzanie czy gracz dotyka ziemi
        if (IsGrounded() && !isGrounded)
        {
            isGrounded = true;
            jumpCount = 0; // Reset skoków tylko raz po dotkniêciu ziemi
        }
        else if (!IsGrounded())
        {
            isGrounded = false;
        }

        // Logika skakania
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpCount++; // Zwiêksz licznik skoków
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
}
