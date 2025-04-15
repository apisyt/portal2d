using UnityEngine;

public class SpawnerOnTrigger2DNew : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject prefabToSpawn;  // Prefab do spawnowania
    public Transform spawnPoint;      // Punkt spawnowania (mo�esz ustawi� np. puste GameObject w scenie)

    private bool isSpawned = false;

    // Zmiana nazwy metody, aby unikn�� konfliktu
    private void OnTriggerEnter2DSpawn(Collider2D other)
    {
        // Je�li prefab jeszcze si� nie pojawi�
        if (!isSpawned && other.CompareTag("Player")) // Zak�adaj�c, �e obiekt ma tag "Player"
        {
            Debug.Log("Trigger Entered! Spawning prefab.");
            SpawnPrefab();
        }
    }

    private void SpawnPrefab()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            // Spawnowanie prefab bez pr�dko�ci i innych efekt�w
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
            isSpawned = true; // Zmieniamy flag�, �eby prefab pojawi� si� tylko raz
        }
        else
        {
            Debug.LogError("Prefab or spawnPoint is not assigned!");
        }
    }
}
