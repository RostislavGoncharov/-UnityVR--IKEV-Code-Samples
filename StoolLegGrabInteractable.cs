using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoolLegGrabInteractable : XRGrabInteractableExtraAttach
{
    bool attachedToTop = false;

    public delegate void SetOpposingLegSocketActive(bool activeValue);
    public SetOpposingLegSocketActive onDeactivateOpposingLegSocket;
    
    public void OnPartInteraction(bool value)
    {
        if (attachedToTop)
        {
            onDeactivateOpposingLegSocket?.Invoke(value);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("StoolTopSocket"))
        {
            attachedToTop = true;
        }

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        attachedToTop = false;

        ExtendedSocketInteractor socket = GetComponentInChildren<ExtendedSocketInteractor>();
        socket.enabled = true;

        base.OnSelectExited(args);
    }
}
