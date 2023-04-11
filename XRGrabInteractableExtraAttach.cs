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

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Hand"))
        {
            attachTransform = grabTransform;
        }

        base.OnSelectEntered(args);
    }

    public void SetAttachTransform()
    {
        attachTransform = socketAttachTransform;
    }

    public void SetGrabTransform()
    {
        attachTransform = grabTransform;
    }
}
