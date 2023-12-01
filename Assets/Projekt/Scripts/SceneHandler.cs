using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private string changeToSceneName;
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadAssigment(string sceneName)
    {
        switch (sceneName.ToLower())
        {
            case "algebra":
                SceneManager.LoadScene("Algebra");
                break;
            default:
                break;
        }
    }
}
