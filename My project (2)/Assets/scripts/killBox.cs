using UnityEngine;

public class killbox : MonoBehaviour
{
    [Header("Ustawienia obra¿eñ")]
    public int damage = 100; // Iloœæ obra¿eñ, które zadaje poci¹g

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy obiekt ma tag "Player"
        if (other.CompareTag("Player"))
        {
            // Pobranie komponentu PlayerHealth z obiektu gracza
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Zadaj obra¿enia graczowi
                Debug.Log("Poci¹g zada³ graczowi " + damage + " obra¿eñ!");
            }
            else
            {
                Debug.LogWarning("Obiekt gracza nie ma komponentu PlayerHealth!");
            }
        }
    }
}
