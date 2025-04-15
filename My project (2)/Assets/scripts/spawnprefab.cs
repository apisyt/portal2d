using UnityEngine;

public class SpawnerOnTrigger2DNew : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject prefabToSpawn;  // Prefab do spawnowania
    public Transform spawnPoint;      // Punkt spawnowania (mo¿esz ustawiæ np. puste GameObject w scenie)

    private bool isSpawned = false;

    // Zmiana nazwy metody, aby unikn¹æ konfliktu
    private void OnTriggerEnter2DSpawn(Collider2D other)
    {
        // Jeœli prefab jeszcze siê nie pojawi³
        if (!isSpawned && other.CompareTag("Player")) // Zak³adaj¹c, ¿e obiekt ma tag "Player"
        {
            Debug.Log("Trigger Entered! Spawning prefab.");
            SpawnPrefab();
        }
    }

    private void SpawnPrefab()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            // Spawnowanie prefab bez prêdkoœci i innych efektów
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
            isSpawned = true; // Zmieniamy flagê, ¿eby prefab pojawi³ siê tylko raz
        }
        else
        {
            Debug.LogError("Prefab or spawnPoint is not assigned!");
        }
    }
}
