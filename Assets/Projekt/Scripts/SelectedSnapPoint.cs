using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectedSnapPoint : XRSocketInteractor
{
    public GameObject attachedObject;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        GameObject interactObject = args.interactableObject.transform.gameObject;
        DeleteNumber? deleteNumber = interactObject.GetComponent<DeleteNumber>();
        if (deleteNumber != null)
        {
            attachedObject = interactObject;
            deleteNumber.Snapped();
        }
        
    }
}
