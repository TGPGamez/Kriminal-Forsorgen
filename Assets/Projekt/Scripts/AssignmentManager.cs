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
    [SerializeField] private TextMeshProUGUI assignmentInfo;
    [SerializeField] private TextMeshProUGUI assignmentText;
    [SerializeField] private TextMeshProUGUI moduleText;
    [SerializeField] private List<AlgeBraAssignment> assignments;
    [SerializeField] private GameObject answerArea;
    [SerializeField] private GameObject answerPrefab;
    [SerializeField] private GameObject correctAnswerBoard;
    private int amountOfAssignments;
    private int currentAssignmentCount;
    private AlgeBraAssignment currentAssignment;
    public AlgeBraAssignment CurrentAssignment { get { return currentAssignment; } }

    // Start is called before the first frame update
    void Start()
    {
        moduleText.text = moduleName;
        LoadAssignments();
        amountOfAssignments = assignments.Count;
        currentAssignmentCount = 1;
        currentAssignment = assignments.First();
        GenerateAnswerBoxes();
        UpdateCanvases();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void LoadAssignments()
    { //"3(8+2−4)", "18", "8 * (5 + 10)", "120"
        assignments.Add(AlgeBraAssignment.CreateComponent(gameObject, "3(8+2−4)", "18"));
        assignments.Add(AlgeBraAssignment.CreateComponent(gameObject, "8 * (5 + 10)", "120"));
    }

    public void UpdateCanvases()
    {
        UpdateAssignmentInfo();
        UpdateAssignment();
    }

    private void UpdateAssignmentInfo()
    {
        if (assignmentInfo != null)
        {
            assignmentInfo.text = $"Opgave {currentAssignmentCount}/{amountOfAssignments}";
        }
    }

    private void UpdateAssignment()
    {
        if (assignmentText != null)
        {
            assignmentText.text = $"{currentAssignment.AssignmentText}";
        }
    }

    public void NextAssignment()
    {
        if (!LastAssignment())
        {
            currentAssignmentCount++;
            assignments.RemoveAt(0);
            currentAssignment = assignments.First();
            correctAnswerBoard.SetActive(false);
            RemoveAnswerBoxes();
            GenerateAnswerBoxes();
            UpdateCanvases();
        } else
        {
            SceneManager.LoadScene("ChooseModule");
        }
    }


    private void GenerateAnswerBoxes()
    {
        if (answerArea != null && answerPrefab != null)
        {
            GameObject last = null;
            Vector3 areaVector = answerArea.transform.position;
            for (int i = 0; i < currentAssignment.Result.Length; i++)
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
        return currentAssignmentCount == amountOfAssignments;
    }
}
