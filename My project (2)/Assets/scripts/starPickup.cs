using UnityEngine;
using UnityEngine.UI;

public class StarPickup : MonoBehaviour
{
    public Text starCounterText;
    public AudioClip pickupSound;

    private int starCount = 0;
    private int autoStarLimit = 6; // maksymalna liczba gwiazdek przez auto-regeneracjê
    private float autoAddTimer = 10f; // co ile sekund dodaje siê gwiazdka
    private float timer = 0f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdateStarCounter();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Co 30 sekund dodaj gwiazdkê, jeœli jest mniej ni¿ limit
        if (timer >= autoAddTimer && starCount < autoStarLimit)
        {
            starCount++;
            UpdateStarCounter();
            timer = 0f;
        }
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
