using UnityEngine;

public class TeleportPlayer2D : MonoBehaviour
{
    // Publiczne pole, które pozwala na ustawienie pozycji teleportacji w inspektorze
    [SerializeField] private Vector3 teleportPosition = new Vector3(0, 0, 0);

    // Funkcja wywo³ywana, gdy inny Collider2D wejdzie w trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzamy, czy obiekt, który wszed³ w trigger, ma tag "Player"
        if (other.CompareTag("Player"))
        {
            // Teleportujemy gracza do ustawionej pozycji
            other.transform.position = teleportPosition;

            // Wypisujemy komunikat do konsoli (opcjonalnie)
            Debug.Log($"Gracz zosta³ teleportowany do {teleportPosition}!");
        }
    }
}
