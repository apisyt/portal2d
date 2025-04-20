using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Wróg dosta³: " + amount + " DMG (pozosta³o: " + currentHealth + ")");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Mo¿esz dodaæ animacjê, efekty, itd.
        Destroy(gameObject);
    }
}
