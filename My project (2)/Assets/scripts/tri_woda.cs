using System.Collections;
using UnityEngine;

public class SpawnerOnTrigger2D : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject prefabToSpawn;  // Prefab do spawnowania
    public Transform spawnPoint;      // Punkt spawnowania (mo�esz ustawi� np. puste GameObject w scenie)

    [Header("Timing Settings")]
    public float spawnDuration = 5f;  // Jak d�ugo spawnowa� obiekty (w sekundach)
    public float spawnRate = 1f;      // Liczba obiekt�w do spawnowania na sekund� (ilo�� na sekund�)

    [Header("Movement Settings")]
    public float minSpeed = 0.1f;    // Minimalna pr�dko��
    public float maxSpeed = 0.5f;    // Maksymalna pr�dko��

    private bool isSpawning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isSpawning && other.CompareTag("Player")) // Zak�adaj�c, �e obiekt ma tag "Player"
        {
            Debug.Log("Trigger Entered! Starting spawn process.");
            StartCoroutine(SpawnObjects());
        }
    }

    private IEnumerator SpawnObjects()
    {
        isSpawning = true;

        float elapsedTime = 0f;
        float timeBetweenSpawns = 1f / spawnRate;  // Czas mi�dzy spawnowaniem obiekt�w

        while (elapsedTime < spawnDuration)
        {
            Debug.Log("Spawning prefab...");

            if (prefabToSpawn != null && spawnPoint != null)
            {
                // Spawnowanie prefab
                GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

                // Dodanie losowej pr�dko�ci w losowym kierunku
                Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Losowanie kierunku
                    Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

                    // Losowanie pr�dko�ci
                    float randomSpeed = Random.Range(minSpeed, maxSpeed);

                    // Ustawienie pr�dko�ci obiektu
                    rb.velocity = randomDirection * randomSpeed;

                    Debug.Log("Spawned object with velocity: " + rb.velocity);
                }
                else
                {
                    Debug.LogError("Prefab doesn't have a Rigidbody2D component!");
                }
            }
            else
            {
                Debug.LogError("Prefab or spawnPoint is not assigned!");
            }

            elapsedTime += timeBetweenSpawns;
            yield return new WaitForSeconds(timeBetweenSpawns); // Czekaj okre�lon� ilo�� czasu
        }

        isSpawning = false;
    }
}
