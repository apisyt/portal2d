using UnityEngine;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    [Tooltip("Lista prefabów pokoi (np. Room_01, Room_02...)")]
    public List<GameObject> roomPrefabs;

    private Vector3 nextSpawnPosition = Vector3.zero;

    void Start()
    {
        // Generujemy pierwszy pokój od razu
        SpawnNextRoom();
    }

    public void SpawnNextRoom()
    {
        // Wybierz losowy prefab pokoju
        int i = Random.Range(0, roomPrefabs.Count);
        GameObject prefab = roomPrefabs[i];

        // Zainstantjuj na pozycji nextSpawnPosition
        GameObject room = Instantiate(prefab, nextSpawnPosition, Quaternion.identity);

        // ZnajdŸ ExitPoint prefabowego pokoju, aby wyznaczyæ nastêpn¹ pozycjê
        Transform exit = room.transform.Find("ExitPoint");
        if (exit != null)
        {
            nextSpawnPosition = exit.position;
        }
        else
        {
            Debug.LogWarning("Brak ExitPoint w prefabie: " + prefab.name);
        }

        // Przypnij referencjê RoomSpawner do triggera wewn¹trz pokoju
        RoomEndTrigger trigger = room.GetComponentInChildren<RoomEndTrigger>();
        if (trigger != null)
            trigger.roomSpawner = this;
    }
}