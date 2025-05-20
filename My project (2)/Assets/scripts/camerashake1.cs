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
            // Oblicz si³ê impulsu kamery (0-1)
            float strength = Mathf.InverseLerp(landingVelocityThreshold, 20f, fallSpeed);

            // Generuj impuls kamery
            impulseSource.GenerateImpulse(strength);

            // Oblicz si³ê wibracji proporcjonalnie do prêdkoœci l¹dowania
            float vibrationStrength = Mathf.Lerp(0.3f, 1.0f, strength);

            // Wywo³aj wibracjê (moc lewej i prawej strony taka sama, czas 0.15 s)
            VibrationManager.Instance.Vibrate(vibrationStrength, vibrationStrength, 0.15f);
        }

        wasGrounded = isGrounded;
    }
}
