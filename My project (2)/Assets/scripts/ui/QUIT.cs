using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        // Dzia�a po zbudowaniu gry (build)
        Application.Quit();

        // Pomocne podczas test�w w edytorze
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
