using UnityEngine;

public class KwasDamage2D : MonoBehaviour
{
    public int damagePerSecond = 1;
    public float damageInterval = 1f;

    private float nextDamageTime = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && Time.time >= nextDamageTime)
            {
                playerHealth.TakeDamage(damagePerSecond);
                Debug.Log("Gracz dosta³ obra¿enia od kwasu (2D trigger).");
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nextDamageTime = 0f;
        }
    }
}
