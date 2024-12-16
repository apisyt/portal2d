using UnityEngine;
using UnityEngine.SceneManagement; // Do resetowania sceny

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksymalne HP
    public int currentHealth; // Aktualne HP

    // Definicja eventu, który bêdzie wywo³ywany, gdy zdrowie gracza siê zmieni
    public delegate void HealthChanged(int currentHealth, int maxHealth);
    public event HealthChanged OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth; // Ustawiamy pocz¹tkowe zdrowie na maksymalne
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die(); // Jeœli zdrowie gracza spadnie do 0, wywo³aj metodê Die
        }
    }

    // Metoda do zadawania obra¿eñ
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Odejmuje obra¿enia od zdrowia
        if (currentHealth < 0) currentHealth = 0; // Zapewnia, ¿e zdrowie nie spadnie poni¿ej 0

        // Wywo³anie eventu po zmianie zdrowia
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Metoda do leczenia gracza
    public void Heal(int amount)
    {
        currentHealth += amount; // Dodaje zdrowie
        if (currentHealth > maxHealth) currentHealth = maxHealth; // Zapewnia, ¿e zdrowie nie przekroczy maksymalnego

        // Wywo³anie eventu po zmianie zdrowia
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Metoda wywo³ywana, gdy gracz umiera (czyli gdy HP spada do 0)
    private void Die()
    {
        Debug.Log("Gracz umar³! Resetowanie sceny...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // £aduje ponownie scenê
    }
}
