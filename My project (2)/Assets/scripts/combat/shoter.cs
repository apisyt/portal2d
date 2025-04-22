using UnityEngine;
using System.Collections; // Dodajemy przestrze� nazw dla IEnumerator

public class PlayerShooting : MonoBehaviour
{
    [Header("Strzelanie")]
    public GameObject projectilePrefab;
    public Transform firePoint; // Miejsce pojawienia si� pocisku
    public float cooldown = 0.5f;
    public float projectileSpeed = 10f;
    public int projectileDamage = 10;
    public float shootDelay = 0.2f; // Op�nienie przed strza�em (w sekundach)

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private float lastShootTime = 0f;
    private bool isFacingRight = true;
    private bool isShooting = false;

    void Update()
    {
        // Uaktualnienie kierunku patrzenia gracza
        if (transform.localScale.x > 0f)
            isFacingRight = true;
        else if (transform.localScale.x < 0f)
            isFacingRight = false;

        // Strza� przy Q, z uwzgl�dnieniem cooldown
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= lastShootTime + cooldown)
        {
            // Uruchomienie op�nienia przed strza�em
            StartCoroutine(ShootWithDelay());
            lastShootTime = Time.time;
        }
    }

    private IEnumerator ShootWithDelay()
    {
        // Wyzwolenie animacji strza�u
        animator.SetTrigger("Shoot");

        // Op�nienie przed wystrza�em
        yield return new WaitForSeconds(shootDelay);

        // Tworzenie pocisku
        Vector3 spawnPosition = firePoint.position;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // Ustawienie pr�dko�ci i obra�e� pocisku
        Projectile proj = projectile.GetComponent<Projectile>();
        int direction = isFacingRight ? 1 : -1;
        proj.Initialize(projectileSpeed * direction, projectileDamage);
    }
}
