using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketEvents : XRSocketInteractor
{
    protected IXRSelectInteractable attachedObject;
    [SerializeField] string correctPartTag;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public virtual void Attach()
    {
        attachedObject = this.GetOldestInteractableSelected();
        attachedObject.transform.GetComponent<Rigidbody>().WakeUp();
        attachedObject.transform.GetComponent<Rigidbody>().isKinematic = false;
    }

    public virtual void Detach()
    {

    }

    public void IncrementAttachments()
    {
        if (attachedObject.transform.CompareTag(correctPartTag))
        {
            GameManager.Instance.IncrementAttachments();
        }   
    }

    public void DecrementAttachments()
    {
        GameManager.Instance.DecrementAttachments();
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        XRGrabInteractableExtraAttach partToAttach = (XRGrabInteractableExtraAttach)args.interactableObject;

        if (partToAttach)
        {
            partToAttach.canBeAttached = true;
        }

        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        XRGrabInteractableExtraAttach partToAttach = (XRGrabInteractableExtraAttach)args.interactableObject;

        if (partToAttach)
        {
            partToAttach.canBeAttached = false;
        }

        base.OnHoverExited(args);
    }

}
