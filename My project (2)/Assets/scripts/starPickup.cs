using UnityEngine;
using UnityEngine.UI;

public class StarPickup : MonoBehaviour
{
    public Text starCounterText;  // Referencja do licznika w UI
    public AudioClip pickupSound; // D�wi�k podnoszenia gwiazdki

    private int starCount = 0;    // Liczba zebranych gwiazdek
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdateStarCounter();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup"))
        {
            starCount++;
            UpdateStarCounter();

            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            Destroy(other.gameObject);
        }
    }

    private void UpdateStarCounter()
    {
        if (starCounterText != null)
        {
            starCounterText.text = "Stars: " + starCount.ToString();
        }
    }

    // NOWA METODA � pr�buj u�y� gwiazdk�
    public bool TryUseStar()
    {
        if (starCount > 0)
        {
            starCount--;
            UpdateStarCounter();
            return true;
        }
        return false;
    }
}
