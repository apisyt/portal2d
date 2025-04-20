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
        Debug.Log("Wr�g dosta�: " + amount + " DMG (pozosta�o: " + currentHealth + ")");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Mo�esz doda� animacj�, efekty, itd.
        Destroy(gameObject);
    }
}
