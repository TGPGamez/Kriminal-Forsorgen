using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResultHandler : MonoBehaviour
{
    [SerializeField] private AssignmentManager assignmentManager;
    [SerializeField] private GameObject answerArea;
    [SerializeField] private List<GameObject> markResponseAnswerObjects;
    [SerializeField] private Material wrongAnswerMaterial;
    [SerializeField] private Material correctAnswerMaterial;
    [SerializeField] private GameObject correctAnswerBoard;

    /// <summary>
    /// Check result and execute actions
    /// </summary>
    public void CheckResult()
    {
        //Get snap points
        List<SelectedSnapPoint> snapPoints = GetAttachedSnappedObjects();
        //Create result out from the snap points
        string result = AttachedSnappedObjectsToResult(snapPoints);
        //Check if result is correct
        if (assignmentManager.IsCorrectAnswer(result))
        {
            //If correct, then hightlight correct and prompt user
            HighlightAnswerResult(true);
            LastAssignmentSetText();
            correctAnswerBoard.SetActive(true);
        } else
        {
            //Highlight is wrong answer
            HighlightAnswerResult(false);
        }
    }

    /// <summary>
    /// Highlight specific elements if answer is correct or wrong
    /// </summary>
    /// <param name="correct"></param>
    private void HighlightAnswerResult(bool correct)
    {
        foreach (GameObject obj in markResponseAnswerObjects)
        {
            //Get all MeshRenderer's and loop through
            foreach (MeshRenderer mesh in obj.GetComponentsInChildren<MeshRenderer>())
            {
                //Save original material
                Material original = new(mesh.material);
                //Set material to either right or wrong answer material
                mesh.material = correct ? correctAnswerMaterial : wrongAnswerMaterial;
                //Start coroutine that turns material back to original
                StartCoroutine(BackToOriginalMat(mesh, original));
            }
        }
    }

    /// <summary>
    /// Turns the meshrenderer back to original material
    /// </summary>
    /// <param name="meshRenderer"></param>
    /// <param name="original"></param>
    /// <returns></returns>
    private IEnumerator BackToOriginalMat(MeshRenderer meshRenderer, Material original)
    {
        yield return new WaitForSeconds(3f);
        if (meshRenderer != null)
        {
            meshRenderer.material = original;
        }
    }

    /// <summary>
    /// Get elements from answerarea that has SelectedSnapPoint script attached
    /// </summary>
    /// <returns>List of SelectedSnapPoint</returns>
    private List<SelectedSnapPoint> GetAttachedSnappedObjects()
    {
        return answerArea.GetComponentsInChildren<SelectedSnapPoint>().ToList();
    }

    /// <summary>
    /// Converts the list of SelectedSnapPoint into result
    /// </summary>
    /// <param name="snapPoints"></param>
    /// <returns></returns>
    private string AttachedSnappedObjectsToResult(List<SelectedSnapPoint> snapPoints)
    {
        string result = string.Empty;
        foreach (SelectedSnapPoint snapPoint in snapPoints)
        {
            //if attachedObject is not null, then get and add the text from the TextMeshProUGui script
            if (snapPoint.attachedObject == null) continue;
            result += snapPoint.attachedObject.GetComponentInChildren<TextMeshProUGUI>().text;
        }
        return result;
    }

    /// <summary>
    /// Method to change text on correct answer board
    /// </summary>
    private void LastAssignmentSetText()
    {
        //Check if last assignment
        if (assignmentManager.LastAssignment())
        {
            //Change text
            correctAnswerBoard.GetComponentInChildren<TextMeshProUGUI>().text = "Du har klaret alle opgaver.\nTryk for at forsætte";
        }
    }
}
