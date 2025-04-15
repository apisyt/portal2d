using UnityEngine;

public class Checkpoint2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Sprawdza, czy obiekt ma tag "Player"
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.SetCheckpoint(transform.position);
                Debug.Log("Checkpoint osi¹gniêty na pozycji: " + transform.position);
            }
        }
    }
}
