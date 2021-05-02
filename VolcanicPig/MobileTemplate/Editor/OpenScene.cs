using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

public class OpenScene : MonoBehaviour
{
    private const string InitScenePath = "Assets/Module-CasualMobileTemplate/VolcanicPig/MobileTemplate/Scenes/InitScene.unity"; 

    [MenuItem("Volcanic Pig/Open Init Scene")]
    static void OpenInitScene()
    {
        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorApplication.OpenScene(InitScenePath);
    }
}
