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
    private AssigmentMockModel currentAssigment;
    private AssigmentMockModel nextAssignment;

    private void Awake()
    {
        //Recieve current assignment
    }

    // Start is called before the first frame update
    void Start()
    {
        moduleText.text = moduleName;
        GenerateAnswerBoxes();
        UpdateCanvases();
        //Load next assigment
    }

    //private void LoadAssignments()
    //{ //"3(8+2−4)", "18", "8 * (5 + 10)", "120"
    //    assignments.Add(AlgeBraAssignment.CreateComponent(gameObject, "3(8+2−4)", "18"));
    //    assignments.Add(AlgeBraAssignment.CreateComponent(gameObject, "8 * (5 + 10)", "120"));
    //}

    public void UpdateCanvases()
    {
        UpdateAssignmentInfo();
        UpdateAssignment();
    }

    private void UpdateAssignmentInfo()
    {
        if (assignmentName != null)
        {
            assignmentName.text = $"{currentAssigment.Name}";
        }
    }

    private void UpdateAssignment()
    {
        if (assignmentText != null)
        {
            assignmentText.text = $"{currentAssigment.Question}";
        }
    }

    public void NextAssignment()
    {
        if (resetSpawners != null) resetSpawners.ResetAll();
        if (!LastAssignment())
        {
            currentAssigment = nextAssignment;
            correctAnswerBoard.SetActive(false);
            RemoveAnswerBoxes();
            GenerateAnswerBoxes();
            UpdateCanvases();
        } else
        {
            SceneManager.LoadScene("ChooseSubject");
        }
    }


    private void GenerateAnswerBoxes()
    {
        if (answerArea != null && answerPrefab != null)
        {
            GameObject last = null;
            Vector3 areaVector = answerArea.transform.position;
            for (int i = 0; i < nextAssignment.Answer.Length; i++)
            {

                Vector3 spawnVector = last == null ?
                    new Vector3(areaVector.x - 0.1f, areaVector.y, areaVector.z) :
                    new Vector3(last.transform.position.x - 0.15f, areaVector.y, areaVector.z);
                last = Instantiate(answerPrefab, spawnVector, Quaternion.identity, answerArea.transform);
            }
        }
    }

    private void RemoveAnswerBoxes()
    {
        foreach (GameObject item in SceneManager.GetActiveScene().GetRootGameObjects().Where(x => x.tag.Equals("NumberObject")))
        {
            Destroy(item);
        };
        foreach (Transform transform in answerArea.transform)
        {
            Destroy(transform.gameObject);
        }
    }

    public bool LastAssignment()
    {
        return currentAssigment.NextAssignmentId == null;
    }
}
