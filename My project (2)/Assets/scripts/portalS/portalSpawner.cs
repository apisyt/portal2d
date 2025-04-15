using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bluePortalPrefab;
    [SerializeField] private GameObject greenPortalPrefab;
    [SerializeField] private KeyCode spawnBlueKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode spawnGreenKey = KeyCode.Mouse1;
    [SerializeField] private StarPickup starPickup; // Dodaj referencjê do skryptu gwiazdek

    private GameObject lastBluePortal;
    private GameObject lastGreenPortal;

    void Update()
    {
        if (Input.GetKeyDown(spawnBlueKey))
        {
            SpawnPortal(bluePortalPrefab, ref lastBluePortal);
        }

        if (Input.GetKeyDown(spawnGreenKey))
        {
            SpawnPortal(greenPortalPrefab, ref lastGreenPortal);
        }
    }

    private void SpawnPortal(GameObject portalPrefab, ref GameObject lastPortal)
    {
        // SprawdŸ, czy mamy wystarczaj¹co gwiazdek
        if (starPickup == null || !starPickup.TryUseStar())
        {
            Debug.Log("Brak gwiazdek – nie mo¿na postawiæ portalu!");
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (lastPortal != null)
        {
            Destroy(lastPortal);
        }

        GameObject newPortal = Instantiate(portalPrefab, mousePosition, Quaternion.identity);
        lastPortal = newPortal;

        if (portalPrefab == bluePortalPrefab && lastGreenPortal != null)
        {
            LinkPortals(newPortal, lastGreenPortal);
        }
        else if (portalPrefab == greenPortalPrefab && lastBluePortal != null)
        {
            LinkPortals(newPortal, lastBluePortal);
        }
    }

    private void LinkPortals(GameObject portalA, GameObject portalB)
    {
        Portal portalAScript = portalA.GetComponent<Portal>();
        Portal portalBScript = portalB.GetComponent<Portal>();

        if (portalAScript != null && portalBScript != null)
        {
            portalAScript.destination = portalB.transform;
            portalBScript.destination = portalA.transform;
        }
    }
}
