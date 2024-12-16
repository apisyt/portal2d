using UnityEngine;
using UnityEngine.SceneManagement; // Do resetowania sceny

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksymalne HP
    public int currentHealth; // Aktualne HP

    // Definicja eventu, kt�ry b�dzie wywo�ywany, gdy zdrowie gracza si� zmieni
    public delegate void HealthChanged(int currentHealth, int maxHealth);
    public event HealthChanged OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth; // Ustawiamy pocz�tkowe zdrowie na maksymalne
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die(); // Je�li zdrowie gracza spadnie do 0, wywo�aj metod� Die
        }
    }

    // Metoda do zadawania obra�e�
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Odejmuje obra�enia od zdrowia
        if (currentHealth < 0) currentHealth = 0; // Zapewnia, �e zdrowie nie spadnie poni�ej 0

        // Wywo�anie eventu po zmianie zdrowia
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Metoda do leczenia gracza
    public void Heal(int amount)
    {
        currentHealth += amount; // Dodaje zdrowie
        if (currentHealth > maxHealth) currentHealth = maxHealth; // Zapewnia, �e zdrowie nie przekroczy maksymalnego

        // Wywo�anie eventu po zmianie zdrowia
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Metoda wywo�ywana, gdy gracz umiera (czyli gdy HP spada do 0)
    private void Die()
    {
        Debug.Log("Gracz umar�! Resetowanie sceny...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // �aduje ponownie scen�
    }
}
