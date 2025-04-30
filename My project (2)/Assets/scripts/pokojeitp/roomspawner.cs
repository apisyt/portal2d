using UnityEngine;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    [Tooltip("Lista prefabów pokoi (Room_01, Room_02 itd.)")]
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
            prefabToSpawn = roomPrefabs[0]; // pierwszy pokój zawsze ten sam
            firstRoomSpawned = true;
        }
        else
        {
            // losuj, a¿ nie bêdzie taki sam jak poprzedni
            do
            {
                prefabToSpawn = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            }
            while (prefabToSpawn == lastSpawnedRoom);
        }

        GameObject room = Instantiate(prefabToSpawn, nextSpawnPosition, Quaternion.identity);
        lastSpawnedRoom = prefabToSpawn;

        // Oblicz przesuniêcie wzglêdem obecnego pokoju
        Transform exit = room.transform.Find("ExitPoint");
        if (exit != null)
        {
            float roomWidth = exit.position.x - room.transform.position.x;
            nextSpawnPosition += new Vector3(roomWidth, 0f, 0f); // tylko X siê przesuwa, Y zawsze 0
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
