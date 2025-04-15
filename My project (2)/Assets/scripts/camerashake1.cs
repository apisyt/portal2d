using UnityEngine;
using Cinemachine;

public class PlayerLandingImpulse : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private float landingVelocityThreshold = 4f;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private bool wasGrounded;

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float fallSpeed = Mathf.Abs(rb.velocity.y);

        if (!wasGrounded && isGrounded && fallSpeed > landingVelocityThreshold)
        {
            float strength = Mathf.InverseLerp(landingVelocityThreshold, 20f, fallSpeed); // 0–1 w zale¿noœci od prêdkoœci
            impulseSource.GenerateImpulse(strength);
        }

        wasGrounded = isGrounded;
    }
}
