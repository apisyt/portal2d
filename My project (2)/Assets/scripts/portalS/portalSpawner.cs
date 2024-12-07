using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bluePortalPrefab; // Prefab niebieskiego portalu
    [SerializeField] private GameObject greenPortalPrefab; // Prefab zielonego portalu
    [SerializeField] private KeyCode spawnBlueKey = KeyCode.Mouse0; // Klawisz dla niebieskiego portalu (LPM)
    [SerializeField] private KeyCode spawnGreenKey = KeyCode.Mouse1; // Klawisz dla zielonego portalu (PPM)

    private GameObject lastBluePortal; // Ostatni niebieski portal
    private GameObject lastGreenPortal; // Ostatni zielony portal

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
        // Pobierz pozycj� myszy w �wiecie
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ustaw Z na 0, aby portal by� w odpowiedniej p�aszczy�nie

        // Usu� poprzedni portal, je�li istnieje
        if (lastPortal != null)
        {
            Destroy(lastPortal);
        }

        // Stw�rz nowy portal
        GameObject newPortal = Instantiate(portalPrefab, mousePosition, Quaternion.identity);

        // Zaktualizuj referencj� do ostatniego portalu
        lastPortal = newPortal;

        // Ustaw po��czenie mi�dzy portalami, je�li istnieje portal drugiego koloru
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
