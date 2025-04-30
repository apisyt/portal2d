using UnityEngine;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    [Tooltip("Lista prefab�w pokoi (Room_01, Room_02 itd.)")]
    public List<GameObject> roomPrefabs;

    private Vector3 nextSpawnPosition = Vector3.zero;
    private GameObject lastSpawnedRoom;
    private bool firstRoomSpawned = false;

    void Start()
    {
        SpawnNextRoom();
    }

    public void SpawnNextRoom()
    {
        GameObject prefabToSpawn;

        if (!firstRoomSpawned)
        {
            prefabToSpawn = roomPrefabs[0]; // pierwszy pok�j zawsze ten sam
            firstRoomSpawned = true;
        }
        else
        {
            // losuj, a� nie b�dzie taki sam jak poprzedni
            do
            {
                prefabToSpawn = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            }
            while (prefabToSpawn == lastSpawnedRoom);
        }

        GameObject room = Instantiate(prefabToSpawn, nextSpawnPosition, Quaternion.identity);
        lastSpawnedRoom = prefabToSpawn;

        // Oblicz przesuni�cie wzgl�dem obecnego pokoju
        Transform exit = room.transform.Find("ExitPoint");
        if (exit != null)
        {
            float roomWidth = exit.position.x - room.transform.position.x;
            nextSpawnPosition += new Vector3(roomWidth, 0f, 0f); // tylko X si� przesuwa, Y zawsze 0
        }
        else
        {
            Debug.LogWarning("ExitPoint nie znaleziony w: " + prefabToSpawn.name);
        }

        RoomEndTrigger trigger = room.GetComponentInChildren<RoomEndTrigger>();
        if (trigger != null)
            trigger.roomSpawner = this;
    }
}
