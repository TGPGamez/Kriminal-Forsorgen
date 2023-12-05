using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssignmentManager : MonoBehaviour
{
    [SerializeField] private string moduleName;
    [SerializeField] private TextMeshProUGUI assignmentName;
    [SerializeField] private TextMeshProUGUI assignmentText;
    [SerializeField] private TextMeshProUGUI moduleText;
    [SerializeField] private GameObject answerArea;
    [SerializeField] private GameObject answerPrefab;
    [SerializeField] private GameObject correctAnswerBoard;
    [SerializeField] private ResetSpawners resetSpawners;
    [SerializeField] private ApiCaller apiCaller;
    private AssigmentModel currentAssigment;
    private AssigmentModel nextAssignment;
    private void Awake()
    {
        //Recieve current assignment
        //
        currentAssigment = apiCaller.GetAssigment(
                InformationHolder.Get<Guid>("Subject.Id").ToString(),
                InformationHolder.Get<Guid>("Module.Id").ToString(),
                InformationHolder.Get<Guid>("Assignment.Id").ToString()
            );
    }

    // Start is called before the first frame update
    void Start()
    {
        moduleText.text = moduleName;
        GenerateAnswerBoxes();
        UpdateCanvases();
        SetNextAssignment();
    }

    /// <summary>
    /// Update the different canvases
    /// </summary>
    public void UpdateCanvases()
    {
        UpdateAssignmentInfo();
        UpdateAssignment();
    }
    
    /// <summary>
    /// Update the assignment info canvas
    /// </summary>
    private void UpdateAssignmentInfo()
    {
        if (assignmentName != null)
        {
            assignmentName.text = $"{currentAssigment.Name}";
        }
    }

    /// <summary>
    /// Update the Assignment canvas
    /// </summary>
    private void UpdateAssignment()
    {
        if (assignmentText != null)
        {
            assignmentText.text = $"{currentAssigment.Question}";
        }
    }

    /// <summary>
    /// Method to set next assignment or go back to modules if last assignment completed 
    /// </summary>
    public void NextAssignment()
    {
        //Reset all spawners so all numbers are generated
        if (resetSpawners != null) resetSpawners.ResetAll();
        //Check if it is the last assignment in the module
        if (!LastAssignment())
        {
            //Reset the different elements in the scene
            currentAssigment = nextAssignment;
            correctAnswerBoard.SetActive(false);
            RemoveAnswerBoxes();
            GenerateAnswerBoxes();
            UpdateCanvases();
            SetNextAssignment();
        } else
        {
            SceneManager.LoadScene("ChooseModule");
        }
    }

    /// <summary>
    /// Set the next assignment from api out from current assignment NextAssignmentId
    /// </summary>
    private void SetNextAssignment()
    {
        //Check if there is a NextAssignmentId
        if (currentAssigment.NextAssignmentId != Guid.Empty)
        {
            nextAssignment = apiCaller.GetAssigment(
                    InformationHolder.Get<Guid>("Subject.Id").ToString(),
                    InformationHolder.Get<Guid>("Module.Id").ToString(),
                    currentAssigment.NextAssignmentId.ToString()
                );
        }
    }

    /// <summary>
    /// Generate answer boxes
    /// </summary>
    private void GenerateAnswerBoxes()
    {
        //Check if area and prefab is set
        if (answerArea != null && answerPrefab != null)
        {
            GameObject last = null;
            Vector3 areaVector = answerArea.transform.position;
            //Generate x amount of boxes, amount depends on the length of answer
            for (int i = 0; i < currentAssigment.Answer.Length; i++)
            {
                //Set spawnVector of object
                Vector3 spawnVector = last == null ?
                    new Vector3(areaVector.x - 0.1f, areaVector.y, areaVector.z) :
                    new Vector3(last.transform.position.x - 0.15f, areaVector.y, areaVector.z);
                //Istantiate the object with prefab, spawnvector and parent
                last = Instantiate(answerPrefab, spawnVector, Quaternion.identity, answerArea.transform);
            }
        }
    }

    /// <summary>
    /// Remove all the answer boxes
    /// </summary>
    private void RemoveAnswerBoxes()
    {
        //Remove all NumberObjects in the root parent
        foreach (GameObject item in SceneManager.GetActiveScene().GetRootGameObjects().Where(x => x.tag.Equals("NumberObject")))
        {
            Destroy(item);
        };
        //Remove all objects in the answerArea
        foreach (Transform transform in answerArea.transform)
        {
            Destroy(transform.gameObject);
        }
    }

    /// <summary>
    /// Check if last assignment
    /// </summary>
    /// <returns>boolean</returns>
    public bool LastAssignment()
    {
        return currentAssigment.NextAssignmentId == Guid.Empty;
    }

    /// <summary>
    /// Check if correct answer
    /// </summary>
    /// <param name="result"></param>
    /// <returns>boolean</returns>
    public bool IsCorrectAnswer(string result)
    {
        return result.Equals(currentAssigment.Answer);
    }


   
}
