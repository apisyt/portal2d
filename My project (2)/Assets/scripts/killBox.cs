using UnityEngine;

public class TeleportPlayer2D : MonoBehaviour
{
    // Funkcja wywo³ywana, gdy inny Collider2D wejdzie w trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzamy, czy obiekt, który wszed³ w trigger, ma tag "Player"
        if (other.CompareTag("Player"))
        {
            // Teleportujemy gracza do pozycji (0, 0, 0)
            other.transform.position = new Vector3(0, 0, 0);

            // Wypisujemy komunikat do konsoli (opcjonalnie)
            Debug.Log("Gracz zosta³ teleportowany do (0, 0, 0)!");
        }
    }
}
