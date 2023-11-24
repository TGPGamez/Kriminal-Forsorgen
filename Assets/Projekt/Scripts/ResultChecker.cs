using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ResultChecker : MonoBehaviour
{
    [SerializeField] private AssignmentManager assignmentManager;
    [SerializeField] private GameObject answerArea;
    public void CheckResult()
    {
        Debug.Log("Checkresult method started...");
        GetResultGameObjectsFromAnswerArea();
        Debug.Log("Checkresult method ended...");
    }

    private List<GameObject> GetResultGameObjectsFromAnswerArea()
    {
        Debug.Log("Checking if answerArea is null..");
        if (answerArea != null)  return new List<GameObject>();
        Debug.Log("AnswerArea wasn't null");
        List<GameObject> boxes = answerArea.GetChildren();
        Debug.Log("Amount: " + boxes.Count);
        foreach (GameObject obj in boxes)
        {
            Debug.Log(obj.name);
        }
        return new List<GameObject>();
    }

    public void Test(string test)
    {
        Debug.Log(test);
    }
}
