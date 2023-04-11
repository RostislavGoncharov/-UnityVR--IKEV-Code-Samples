using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
}
