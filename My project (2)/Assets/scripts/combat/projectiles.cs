using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private int damage;
    private Rigidbody2D rb;

    public float lifeTime = 3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); // automatyczne niszczenie
    }

    public void Initialize(float directionSpeed, int projectileDamage)
    {
        speed = directionSpeed;
        damage = projectileDamage;
        rb.velocity = new Vector2(speed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject); // niszczy pocisk po trafieniu
        }
    }
}
