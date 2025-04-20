using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Strzelanie")]
    public GameObject projectilePrefab;
    public Transform firePoint; // Tu ustawiasz miejsce pojawienia siê pocisku
    public float cooldown = 0.5f;
    public float projectileSpeed = 10f;
    public int projectileDamage = 10;

    private float lastShootTime = 0f;
    private bool isFacingRight = true;

    void Update()
    {
        // Update kierunku patrzenia gracza
        if (transform.localScale.x > 0)
            isFacingRight = true;
        else if (transform.localScale.x < 0)
            isFacingRight = false;

        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= lastShootTime + cooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 spawnPosition = firePoint.position;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // Pobierz skrypt pocisku i ustaw parametry
        Projectile proj = projectile.GetComponent<Projectile>();
        int direction = isFacingRight ? 1 : -1;
        proj.Initialize(projectileSpeed * direction, projectileDamage);
    }
}
