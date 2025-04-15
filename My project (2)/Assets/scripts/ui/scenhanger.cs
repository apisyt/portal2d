using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class SceneButtonLoader : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Nie wybrano sceny do za³adowania.");
        }
    }

#if UNITY_EDITOR
    // Custom Inspector tylko w edytorze
    [CustomEditor(typeof(SceneButtonLoader))]
    public class SceneButtonLoaderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SceneButtonLoader loader = (SceneButtonLoader)target;

            var scenes = EditorBuildSettings.scenes;
            string[] sceneNames = new string[scenes.Length];
            int selectedIndex = 0;

            for (int i = 0; i < scenes.Length; i++)
            {
                string path = scenes[i].path;
                string name = System.IO.Path.GetFileNameWithoutExtension(path);
                sceneNames[i] = name;

                if (name == loader.sceneToLoad)
                    selectedIndex = i;
            }

            selectedIndex = EditorGUILayout.Popup("Scena do za³adowania", selectedIndex, sceneNames);
            loader.sceneToLoad = sceneNames[selectedIndex];

            EditorGUILayout.Space();
            if (GUILayout.Button("Za³aduj teraz (tylko w edytorze)"))
            {
                if (!string.IsNullOrEmpty(loader.sceneToLoad))
                {
                    EditorSceneManager.OpenScene(scenes[selectedIndex].path);
                }
            }

            // Zapisz zmiany
            if (GUI.changed)
            {
                EditorUtility.SetDirty(loader);
            }
        }
    }
#endif
}
