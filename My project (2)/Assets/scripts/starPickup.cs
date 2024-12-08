using UnityEngine;
using UnityEngine.UI; // Umo¿liwia obs³ugê UI

public class StarPickup : MonoBehaviour
{
    public Text starCounterText;  // Referencja do licznika w UI
    public AudioClip pickupSound; // DŸwiêk podnoszenia gwiazdki

    private int starCount = 0;    // Liczba zebranych gwiazdek
    private AudioSource audioSource; // Komponent AudioSource

    private void Start()
    {
        // Dodanie komponentu AudioSource do obiektu Gracza
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdateStarCounter();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup")) // Sprawdzenie, czy to gwiazdka
        {
            starCount++; // Dodaj gwiazdkê do licznika
            UpdateStarCounter(); // Aktualizacja UI

            // Odtworzenie dŸwiêku
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            Destroy(other.gameObject); // Usuñ gwiazdkê z poziomu
        }
    }

    // Aktualizacja tekstu w liczniku gwiazdek
    private void UpdateStarCounter()
    {
        if (starCounterText != null)
        {
            starCounterText.text = "Stars: " + starCount.ToString();
        }
    }
}

