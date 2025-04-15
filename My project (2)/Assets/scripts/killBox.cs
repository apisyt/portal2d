using UnityEngine;

public class killbox : MonoBehaviour
{
    [Header("Ustawienia obra�e�")]
    public int damage = 100; // Ilo�� obra�e�, kt�re zadaje poci�g

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy obiekt ma tag "Player"
        if (other.CompareTag("Player"))
        {
            // Pobranie komponentu PlayerHealth z obiektu gracza
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Zadaj obra�enia graczowi
                Debug.Log("Poci�g zada� graczowi " + damage + " obra�e�!");
            }
            else
            {
                Debug.LogWarning("Obiekt gracza nie ma komponentu PlayerHealth!");
            }
        }
    }
}
