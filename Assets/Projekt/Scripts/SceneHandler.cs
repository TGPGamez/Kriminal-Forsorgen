using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that can change scene in different ways
/// </summary>
public class SceneHandler : MonoBehaviour
{
    [SerializeField] private string changeToSceneName;
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Method to load specific scene out from a type
    /// </summary>
    /// <param name="sceneType">scene type</param>
    public void LoadAssigment(string sceneType)
    {
        switch (sceneType.ToLower())
        {
            case "algebra":
                SceneManager.LoadScene("Algebra");
                break;
            default:
                break;
        }
    }
}
