using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtendedSocketInteractor : XRSocketInteractor
{
    protected XRGrabInteractableExtraAttach attachedObject;
    [SerializeField] string correctPartTag;
    [SerializeField] Vector3 colliderOffset;

    public int taskNumber;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public void IncrementAttachments()
    {
        if (attachedObject.transform.CompareTag(correctPartTag))
        {
            GameManager.Instance.IncrementAttachments(taskNumber);
        }   
    }

    public void DecrementAttachments()
    {
        if (attachedObject.transform.CompareTag(correctPartTag))
        {
            GameManager.Instance.DecrementAttachments(taskNumber);
        }
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

    /*
     * Increment attachments, then take control over the part's box collider.
     * Reparenting the collider is needed to make sure that a half-assembled piece of furniture
     * still has proper collision. 
     * */
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        attachedObject = (XRGrabInteractableExtraAttach)args.interactableObject;

        attachedObject.canBeAttached = false;

        AudioManager.Instance.PlaySoundEffect(0);

        IncrementAttachments();

        if (attachedObject == null)
        {
            return;
        }

        foreach (Transform child in attachedObject.transform)
        {
            if (child.CompareTag("PartCollider"))
            {                
                child.SetParent(this.transform);

                BoxCollider childCollider = child.GetComponent<BoxCollider>();
                child.localPosition = new Vector3(colliderOffset.x * childCollider.size.x, colliderOffset.y * childCollider.size.y, colliderOffset.z * childCollider.size.z);
                child.localRotation = Quaternion.identity;
                Debug.Log(childCollider.attachedRigidbody);

                //Set layer to TempColliders to avoid self-interaction
                child.gameObject.layer = LayerMask.NameToLayer("TempColliders");

            }
        }

        base.OnSelectEntered(args);
    }

    //Decrement attachments, then give the collider back to the part. Reset the collider's layer to enable socket interactions.
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        DecrementAttachments();

        AudioManager.Instance.PlaySoundEffect(1);

        foreach (Transform child in transform)
        {
            if (child.CompareTag("PartCollider"))
            {
                child.SetParent(args.interactableObject.transform);
                child.gameObject.layer = LayerMask.NameToLayer("Grab");
            }
        }

        attachedObject = null;

        base.OnSelectExited(args);
    }
}
