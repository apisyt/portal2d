using UnityEngine;

public class RoomEndTrigger : MonoBehaviour
{
    [HideInInspector] public RoomSpawner roomSpawner;

    private bool triggered = false; // zabezpieczenie przed wielokrotnym aktywowaniem

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            if (roomSpawner != null)
            {
                roomSpawner.SpawnNextRoom();
            }
            Destroy(gameObject); // usuwa trigger po u¿yciu
        }
    }
}
