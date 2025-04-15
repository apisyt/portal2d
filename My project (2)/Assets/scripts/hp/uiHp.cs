using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Text healthText; // Referencja do tekstu w UI
    private PlayerHealth playerHealth;

    void Start()
    {
        // ZnajdŸ gracza i jego komponent PlayerHealth
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.OnHealthChanged += UpdateHealthUI; // Pod³¹cz siê do eventu
        }
        else
        {
            Debug.LogError("Nie znaleziono gracza z tagiem 'Player'");
        }
    }

    // Aktualizacja numerka HP w UI
    void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;
    }
}
