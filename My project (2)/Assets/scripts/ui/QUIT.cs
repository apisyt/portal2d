using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        // Dzia³a po zbudowaniu gry (build)
        Application.Quit();

        // Pomocne podczas testów w edytorze
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
