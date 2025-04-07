using UnityEngine;
using UnityEngine.UI;

public class ParallaxBackground : MonoBehaviour
{
    public Dropdown distanceDropdown; // Dropdown UI
    public GameObject backgroundObject; // Obiekt t�a

    void Start()
    {
        // Ustawienie metody, kt�ra b�dzie reagowa� na zmian� wyboru w dropdownie
        distanceDropdown.onValueChanged.AddListener(OnDistanceChanged);

        // Pocz�tkowa warto�� t�a
        OnDistanceChanged(distanceDropdown.value);
    }

    void OnDistanceChanged(int value)
    {
        // Ustawienie odpowiedniej odleg�o�ci na osi Z
        switch (value)
        {
            case 0: // Bliskie
                backgroundObject.transform.position = new Vector3(backgroundObject.transform.position.x, backgroundObject.transform.position.y, 0);
                break;
            case 1: // �rednie
                backgroundObject.transform.position = new Vector3(backgroundObject.transform.position.x, backgroundObject.transform.position.y, 5);
                break;
            case 2: // Dalekie
                backgroundObject.transform.position = new Vector3(backgroundObject.transform.position.x, backgroundObject.transform.position.y, 10);
                break;
        }
    }
}
