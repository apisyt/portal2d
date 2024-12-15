using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleport : MonoBehaviour
{
    [Header("Numer sceny w Build Settings")]
    [SerializeField] private int sceneIndex;

    public void TeleportToScene()
    {
        // Sprawdzenie, czy indeks sceny jest poprawny
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
            Debug.Log($"Przenoszenie do sceny o indeksie: {sceneIndex}");
        }
        else
        {
            Debug.LogWarning("Nieprawid³owy numer sceny! SprawdŸ Build Settings.");
        }
    }
}
