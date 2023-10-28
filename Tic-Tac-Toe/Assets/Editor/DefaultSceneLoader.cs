using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class DefaultSceneLoader : EditorWindow
{

    private const string defaultScenePath = "Assets/Scenes/Preload.unity";

    static DefaultSceneLoader()
    {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    static void LoadDefaultScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            if (EditorSceneManager.GetActiveScene().path != defaultScenePath)
            {

                PlayerPrefs.SetString("dsl_lastPath", EditorSceneManager.GetActiveScene().path);
                PlayerPrefs.Save();

                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

                EditorApplication.delayCall += () =>
                {
                    EditorSceneManager.OpenScene(defaultScenePath);
                    EditorApplication.isPlaying = true;
                };

                EditorApplication.isPlaying = false;
            }
        }

        if (state == PlayModeStateChange.EnteredEditMode)
        {
            if (PlayerPrefs.HasKey("dsl_lastPath"))
            {
                EditorSceneManager.OpenScene(PlayerPrefs.GetString("dsl_lastPath"));
            }
        }
    }
}