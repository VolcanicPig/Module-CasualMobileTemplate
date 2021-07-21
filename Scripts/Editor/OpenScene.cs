using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

public class OpenScene : MonoBehaviour
{
    private const string InitScenePath = "Assets/Scenes/InitScene.unity"; 
    private const string UiScenePath = "Assets/Scenes/UiScene.unity"; 
    private const string GameScenePath = "Assets/Scenes/GameScene.unity"; 

    [MenuItem("Volcanic Pig/Open Init Scene")]
    static void OpenInitScene()
    {
        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorApplication.OpenScene(InitScenePath);
    }

    [MenuItem("Volcanic Pig/Open UI Scene")]
    static void OpenUiScene()
    {
        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorApplication.OpenScene(UiScenePath);
    }

    [MenuItem("Volcanic Pig/Open Game Scene")]
    static void OpenGameScene()
    {
        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorApplication.OpenScene(GameScenePath);
    }
}
