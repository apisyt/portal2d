using UnityEngine;

public class TeleportPlayer2D : MonoBehaviour
{
    // Publiczne pole, kt�re pozwala na ustawienie pozycji teleportacji w inspektorze
    [SerializeField] private Vector3 teleportPosition = new Vector3(0, 0, 0);

    // Funkcja wywo�ywana, gdy inny Collider2D wejdzie w trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzamy, czy obiekt, kt�ry wszed� w trigger, ma tag "Player"
        if (other.CompareTag("Player"))
        {
            // Teleportujemy gracza do ustawionej pozycji
            other.transform.position = teleportPosition;

            // Wypisujemy komunikat do konsoli (opcjonalnie)
            Debug.Log($"Gracz zosta� teleportowany do {teleportPosition}!");
        }
    }
}
