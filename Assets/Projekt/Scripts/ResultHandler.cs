using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ResultHandler : MonoBehaviour
{
    [SerializeField] private AssignmentManager assignmentManager;
    [SerializeField] private GameObject answerArea;
    [SerializeField] private List<GameObject> markResponseAnswerObjects;
    [SerializeField] private Material wrongAnswerMaterial;
    [SerializeField] private Material correctAnserMaterial;
    [SerializeField] private GameObject correctAnswerBoard;
    public void CheckResult()
    {
        List<SelectedSnapPoint> snapPoints = GetAttachedSnappedObjects();
        string result = AttachedSnappedObjectsToResult(snapPoints);
        if (result.Equals(assignmentManager.CurrentAssignment.Result))
        {
            HighlightAnswerResult(true);
            LastAssignmentSetText();
            correctAnswerBoard.SetActive(true);
        } else
        {
            HighlightAnswerResult(false);
        }
    }

    private void HighlightAnswerResult(bool correct)
    {
        foreach (GameObject obj in markResponseAnswerObjects)
        {
            foreach (MeshRenderer mesh in obj.GetComponentsInChildren<MeshRenderer>())
            {
                Material original = new(mesh.material);
                mesh.material = correct ? correctAnserMaterial : wrongAnswerMaterial;
                StartCoroutine(BackToOriginalMat(mesh, original));
            }
        }
    }

    private IEnumerator BackToOriginalMat(MeshRenderer meshRenderer, Material original)
    {
        yield return new WaitForSeconds(3f);
        meshRenderer.material = original;
    }

    private List<SelectedSnapPoint> GetAttachedSnappedObjects()
    {
        return answerArea.GetComponentsInChildren<SelectedSnapPoint>().ToList();
    }

    private string AttachedSnappedObjectsToResult(List<SelectedSnapPoint> snapPoints)
    {
        string result = string.Empty;
        foreach (SelectedSnapPoint snapPoint in snapPoints)
        {
            if (snapPoint.attachedObject == null) continue;
            result += snapPoint.attachedObject.GetComponentInChildren<TextMeshProUGUI>().text;
        }
        return result;
    }

    private void LastAssignmentSetText()
    {
        if (assignmentManager.LastAssignment())
        {
            correctAnswerBoard.GetComponentInChildren<TextMeshProUGUI>().text = "Du har klaret alle opgaver.\nTryk for at forsætte";
        }
    }
}
