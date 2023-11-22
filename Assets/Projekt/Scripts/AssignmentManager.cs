using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AssignmentManager : MonoBehaviour
{
    [SerializeField] private string moduleName;
    [SerializeField] private TextMeshProUGUI assignmentInfo;
    [SerializeField] private TextMeshProUGUI assignmentText;
    [SerializeField] private TextMeshProUGUI moduleText;
    [SerializeField] private List<AlgeBraAssignment> assignments;
    [SerializeField] private GameObject answerArea;
    [SerializeField] private GameObject answerPrefab;
    private int amountOfAssignments;
    private int currentAssignmentCount;
    private AlgeBraAssignment currentAssignment;

    // Start is called before the first frame update
    void Start()
    {
        moduleText.text = moduleName;
        LoadAssignments();
        amountOfAssignments = assignments.Count;
        currentAssignmentCount = 0;
        currentAssignment = assignments.First();
        if (answerArea != null && answerPrefab != null)
        {;
            GameObject last = null;
            Vector3 areaVector = answerArea.transform.position;
            for (int i = 0; i < currentAssignment.Result.Length; i++)
            {

                Vector3 spawnVector = last == null ? 
                    new Vector3(areaVector.x + 0.3f, areaVector.y, areaVector.z) : 
                    new Vector3(last.transform.position.x - 0.4f, areaVector.y, areaVector.z);
                last = Instantiate(answerPrefab, spawnVector, Quaternion.identity, answerArea.transform);
            }
        }
        UpdateCanvases();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void LoadAssignments()
    {
        assignments.Add(new AlgeBraAssignment("3(8+2−4)", "18"));
        assignments.Add(new AlgeBraAssignment("8 * (5 + 10)", "120"));
    }

    public void UpdateCanvases()
    {
        UpdateAssignmentInfo();
        UpdateAssignment();
        //Generate snap boxes
    }

    public void UpdateAssignmentInfo()
    {
        if (assignmentInfo != null)
        {
            assignmentInfo.text = $"Opgave {currentAssignmentCount+1}/{amountOfAssignments}";
        }
    }

    public void UpdateAssignment()
    {
        if (assignmentText != null)
        {
            assignmentText.text = $"{currentAssignment.AssignmentText}";
        }
    }
}
