using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgeBraAssignment : MonoBehaviour
{
    public string AssignmentText { get; set; }
    public string Result { get; private set; }

    public AlgeBraAssignment(string assignmentText, string result)
    {
        AssignmentText = assignmentText;
        Result = result;
    }
}
