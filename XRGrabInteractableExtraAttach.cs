using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
 * This class extends the XRInteractable functionality
 * by allowing you to assign separate transforms 
 * for grab and socket interactions.
 * USAGE:
 * call SetGrabTransform in Hover Entered
 * and SetAttachTransform in Last Select Exited.
*/

public class XRGrabInteractableExtraAttach : XRGrabInteractable
{
    public Transform grabTransform;
    public Transform socketAttachTransform;

    [SerializeField] InteractionLayerMask partLayers;

    public bool canBeAttached = false;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Hand"))
        {
            attachTransform = grabTransform;
        }

        SetInteractionLayerMask();

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (!canBeAttached)
        {
            SetInteractionLayerMaskToDefault();
        }

        base.OnSelectExited(args);
    }

    public void SetAttachTransform()
    {
        attachTransform = socketAttachTransform;
    }

    public void SetGrabTransform()
    {
        attachTransform = grabTransform;
    }

    public void SetInteractionLayerMaskToDefault()
    {
        interactionLayers = InteractionLayerMask.GetMask("Default");
    }

    public void SetInteractionLayerMask()
    {
        interactionLayers = partLayers;
    }
}
