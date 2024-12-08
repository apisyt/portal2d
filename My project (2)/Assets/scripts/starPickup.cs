using UnityEngine;
using UnityEngine.UI; // Umo�liwia obs�ug� UI

public class StarPickup : MonoBehaviour
{
    public Text starCounterText;  // Referencja do licznika w UI
    private int starCount = 0;    // Zmienna do zliczania gwiazdek

    private void Start()
    {
        UpdateStarCounter();
    }

    // Funkcja wywo�ywana, gdy obiekt wejdzie w trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup")) // Sprawdzenie, czy wszed�e� w itemek
        {
            starCount++; // Dodaj gwiazdk� do licznika
            UpdateStarCounter(); // Zaktualizuj UI licznika

            Destroy(other.gameObject); // Usu� zebrany przedmiot z poziomu
        }
    }

    // Funkcja aktualizuj�ca licznik gwiazdek w UI
    private void UpdateStarCounter()
    {
        if (starCounterText != null)
        {
            starCounterText.text = "Stars: " + starCount.ToString();
        }
    }
}
