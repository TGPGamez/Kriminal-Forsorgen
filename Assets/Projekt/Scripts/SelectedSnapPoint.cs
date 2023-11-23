using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectedSnapPoint : XRSocketInteractor
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject interactObject = args.interactableObject.transform.gameObject;
        DeleteNumber? deleteNumber = interactObject.GetComponent<DeleteNumber>();
        if (deleteNumber != null )
        {
            deleteNumber.Snapped();
        }
        base.OnSelectEntered(args);
    }
}
