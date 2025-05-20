using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Strzelanie")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float cooldown = 0.5f;
    public float projectileSpeed = 10f;
    public int projectileDamage = 10;
    public float shootDelay = 0.2f;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private float lastShootTime = 0f;
    private bool isFacingRight = true;

    void Update()
    {
        // Uaktualnienie kierunku patrzenia
        isFacingRight = transform.localScale.x > 0f;

        // Strza³ po naciœniêciu Q lub przycisku „Shoot”
        if (Input.GetButtonDown("Shoot") && Time.time >= lastShootTime + cooldown)
        {
            StartCoroutine(ShootWithDelay());
            lastShootTime = Time.time;
        }
    }

    private IEnumerator ShootWithDelay()
    {
        // 1) Trigger strza³u
        animator.SetTrigger("Shoot");

        // 2) Je¿eli stoi w miejscu, dodatkowo ShotStand
        float h = Input.GetAxisRaw("Horizontal");
        if (Mathf.Approximately(h, 0f))
            animator.SetTrigger("ShotStand");

        // 3) OpóŸnienie
        yield return new WaitForSeconds(shootDelay);

        // 4) Wystrza³ pocisku
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        Projectile proj = projectile.GetComponent<Projectile>();
        int dir = isFacingRight ? 1 : -1;
        proj.Initialize(projectileSpeed * dir, projectileDamage);
    }
}
