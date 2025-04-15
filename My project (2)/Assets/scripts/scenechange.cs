using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditor;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private SceneField sceneToLoad; // U¿ywa customowego dropdowna

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad.SceneName);
        }
    }
}

[System.Serializable]
public class SceneField
{
    [SerializeField] private string sceneName = "";

    public string SceneName => sceneName;

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            SerializedProperty sceneNameProperty = property.FindPropertyRelative("sceneName");

            List<string> sceneNames = new List<string>();
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    sceneNames.Add(System.IO.Path.GetFileNameWithoutExtension(scene.path));
                }
            }

            int index = Mathf.Max(sceneNames.IndexOf(sceneNameProperty.stringValue), 0);
            index = EditorGUI.Popup(position, label.text, index, sceneNames.ToArray());
            sceneNameProperty.stringValue = sceneNames[index];

            EditorGUI.EndProperty();
        }
    }
#endif
}
