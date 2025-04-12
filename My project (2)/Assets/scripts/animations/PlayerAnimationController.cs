using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
    }
}
