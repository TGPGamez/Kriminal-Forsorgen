using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgeBraAssignment : MonoBehaviour
{
    public string AssignmentText { get; set; }
    public string Result { get; set; }


    public static AlgeBraAssignment CreateComponent(GameObject where, string assignmentText, string result)
    {
        AlgeBraAssignment algeBraAssignment = where.AddComponent<AlgeBraAssignment>();
        algeBraAssignment.AssignmentText = assignmentText;
        algeBraAssignment.Result = result;
        return algeBraAssignment;

    }
}
