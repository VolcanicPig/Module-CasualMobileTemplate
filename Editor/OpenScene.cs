using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class OpenScene : MonoBehaviour
{
    private const string _initScenePath = "Assets/Scenes/InitScene.unity"; 
    private const string _uiScenePath = "Assets/Scenes/UiScene.unity"; 
    private const string _gameScenePath = "Assets/Scenes/GameScene.unity"; 

    [MenuItem("Volcanic Pig/Open Init Scene")]
    static void OpenInitScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene(_initScenePath); 
    }

    [MenuItem("Volcanic Pig/Open UI Scene")]
    static void OpenUiScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene(_uiScenePath); 
    }

    [MenuItem("Volcanic Pig/Open Game Scene")]
    static void OpenGameScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene(_gameScenePath); 
    }
}
