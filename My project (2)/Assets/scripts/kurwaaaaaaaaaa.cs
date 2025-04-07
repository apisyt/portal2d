using UnityEngine;
using UnityEngine.UI;

public class ParallaxBackground : MonoBehaviour
{
    public Dropdown distanceDropdown; // Dropdown UI
    public GameObject backgroundObject; // Obiekt t³a

    void Start()
    {
        // Ustawienie metody, która bêdzie reagowaæ na zmianê wyboru w dropdownie
        distanceDropdown.onValueChanged.AddListener(OnDistanceChanged);

        // Pocz¹tkowa wartoœæ t³a
        OnDistanceChanged(distanceDropdown.value);
    }

    void OnDistanceChanged(int value)
    {
        // Ustawienie odpowiedniej odleg³oœci na osi Z
        switch (value)
        {
            case 0: // Bliskie
                backgroundObject.transform.position = new Vector3(backgroundObject.transform.position.x, backgroundObject.transform.position.y, 0);
                break;
            case 1: // Œrednie
                backgroundObject.transform.position = new Vector3(backgroundObject.transform.position.x, backgroundObject.transform.position.y, 5);
                break;
            case 2: // Dalekie
                backgroundObject.transform.position = new Vector3(backgroundObject.transform.position.x, backgroundObject.transform.position.y, 10);
                break;
        }
    }
}
