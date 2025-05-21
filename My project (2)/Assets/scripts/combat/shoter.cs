using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Strzelanie")]
    public GameObject projectilePrefab;
    public Transform firePoint; // Punkt, z którego wystrzeliwany jest pocisk
    public float cooldown = 0.5f; // Czas odnowienia miêdzy strza³ami
    public float projectileSpeed = 10f;
    public int projectileDamage = 10;
    public float shootDelay = 0.2f; // OpóŸnienie po animacji strza³u

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private float lastShootTime = 0f;
    private bool isFacingRight = true;

    void Update()
    {
        // Sprawdzenie kierunku gracza (lewo/prawo)
        if (transform.localScale.x > 0f)
            isFacingRight = true;
        else if (transform.localScale.x < 0f)
            isFacingRight = false;

        // Obs³uga wejœcia strza³u: InputManager -> "Shoot" (Q lub joystick button 2)
        if (Input.GetButtonDown("Shoot") && Time.time >= lastShootTime + cooldown)
        {
            lastShootTime = Time.time;
            StartCoroutine(ShootWithDelay());
        }
    }

    private IEnumerator ShootWithDelay()
    {
        // Animacja strza³u zawsze
        animator.SetTrigger("Shoot");

        // Animacja strza³u stoj¹c, tylko jeœli gracz siê nie porusza
        float h = Input.GetAxisRaw("Horizontal");
        if (Mathf.Approximately(h, 0f))
        {
            animator.SetTrigger("ShotStand");
        }

        // Czekaj na zakoñczenie czêœci animacji
        yield return new WaitForSeconds(shootDelay);

        // Tworzenie pocisku
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Kierunek na podstawie tego, w któr¹ stronê patrzy gracz
        int direction = isFacingRight ? 1 : -1;
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Initialize(projectileSpeed * direction, projectileDamage);
        }
    }
}
