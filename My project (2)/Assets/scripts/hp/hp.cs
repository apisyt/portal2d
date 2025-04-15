using UnityEngine;
using UnityEngine.SceneManagement; // Do resetowania sceny

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksymalne HP
    public int currentHealth; // Aktualne HP

    private Vector3 lastCheckpoint; // Pozycja ostatniego checkpointu

    // Definicja eventu, który bêdzie wywo³ywany, gdy zdrowie gracza siê zmieni
    public delegate void HealthChanged(int currentHealth, int maxHealth);
    public event HealthChanged OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth; // Ustawiamy pocz¹tkowe zdrowie na maksymalne
        LoadCheckpoint(); // £adujemy zapisany checkpoint
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
        Debug.Log("Gracz umar³! Powrót na checkpoint: " + lastCheckpoint);
        transform.position = lastCheckpoint; // Przeniesienie gracza do ostatniego checkpointu
        currentHealth = maxHealth; // Przywrócenie pe³nego zdrowia
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Metoda do ustawiania checkpointu
    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
        PlayerPrefs.SetFloat("CheckpointX", checkpointPosition.x);
        PlayerPrefs.SetFloat("CheckpointY", checkpointPosition.y);
        PlayerPrefs.SetFloat("CheckpointZ", checkpointPosition.z);
        PlayerPrefs.Save();
        Debug.Log("Nowy checkpoint zapisany: " + lastCheckpoint);
    }

    // Metoda do ³adowania checkpointu
    private void LoadCheckpoint()
    {
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            lastCheckpoint = new Vector3(
                PlayerPrefs.GetFloat("CheckpointX"),
                PlayerPrefs.GetFloat("CheckpointY"),
                PlayerPrefs.GetFloat("CheckpointZ")
            );
        }
        else
        {
            lastCheckpoint = transform.position; // Jeœli brak zapisu, ustaw aktualn¹ pozycjê
        }
    }
}
