using UnityEngine;
using UnityEngine.InputSystem; // U¿ywane do wibracji (wymaga nowego Input Systemu)

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Vibration Settings")]
    public float vibrationIntensity = 0.5f; // 0.0 – 1.0
    public float vibrationDuration = 0.3f; // sekundy

    private void Start()
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
        // Wibracja na padzie
        StartCoroutine(VibrateController(vibrationIntensity, vibrationDuration));

        // Tu mo¿esz dorzuciæ efekt, dŸwiêk itp.
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator VibrateController(float intensity, float duration)
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(intensity, intensity); // low + high freq
            yield return new WaitForSeconds(duration);
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }
}
