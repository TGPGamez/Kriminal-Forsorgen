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
        if (interactObject.TryGetComponent<NumberEvents>(out var deleteNumber))
        {
            attachedObject = interactObject;
            deleteNumber.Snapped();
        }
        
    }
}
