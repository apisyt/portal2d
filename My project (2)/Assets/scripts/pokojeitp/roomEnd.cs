using UnityEngine;

public class RoomEndTrigger : MonoBehaviour
{
    [HideInInspector] public RoomSpawner roomSpawner;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag("Player"))
            return;

        triggered = true;
        roomSpawner?.SpawnNextRoom();
        Destroy(gameObject);
    }
}
