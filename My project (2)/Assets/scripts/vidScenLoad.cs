using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToLoad;

    private bool hasSwitched = false;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            // Od razu przypisujemy event
            videoPlayer.loopPointReached += OnVideoFinished;

            // Upewniamy siê, ¿e odtwarza
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("[VideoSceneLoader] Brak VideoPlayera!");
        }
    }

    void Update()
    {
        if (videoPlayer != null && !videoPlayer.isPlaying && !hasSwitched && videoPlayer.time > 0)
        {
            Debug.Log("[VideoSceneLoader] Detected video ended via Update fallback.");
            SwitchScene();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("[VideoSceneLoader] Video finished via loopPointReached.");
        SwitchScene();
    }

    void SwitchScene()
    {
        if (hasSwitched) return;

        hasSwitched = true;

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("[VideoSceneLoader] Loading scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("[VideoSceneLoader] sceneToLoad is empty!");
        }
    }
}
