using UnityEngine;

public class SpawnerWody : MonoBehaviour
{
    // Prefab, który ma być spawnowany
    public GameObject prefabDoSpawnowania;

    // Ilość obiektów do spawnowania
    public int iloscObiektow = 10;

    void Update()
    {
        // Sprawdzenie, czy wciśnięto lewy klawisz CTRL
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SpawnujObiekty();
        }
    }

    // Funkcja do spawnowania obiektów
    void SpawnujObiekty()
    {
        if (prefabDoSpawnowania == null)
        {
            Debug.LogWarning("Prefab nie jest przypisany!");
            return;
        }

        for (int i = 0; i < iloscObiektow; i++)
        {
            // Spawnujemy obiekt w lokalizacji tego obiektu (Transform.position)
            GameObject nowyObiekt = Instantiate(prefabDoSpawnowania, transform.position, Quaternion.identity);

            // Ustawiamy obiekt jako dziecko spawnera
            nowyObiekt.transform.parent = this.transform;
        }

        Debug.Log($"{iloscObiektow} obiektów zostało zespawnowanych jako dzieci spawnera!");
    }
}
