using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

/* This class extends the XRSocketInteractor functionality
 * by calling the GameManager instance to calculate attachment count.
 * Additionally, it makes the socket the parent of the attached part's collider 
 * (which needs to be a separate object), so that the collider keeps working.
 * In this implementation it only helps with the collisions when 2 parts are attached to each other,
 * while any further parts will still have non-working colliders (good enough for the demo).
 */

public class ExtendedSocketInteractor : XRSocketInteractor
{
    protected XRGrabInteractableExtraAttach attachedObject;
    [SerializeField] List<string> correctPartTags = new List<string>();
    [SerializeField] Vector3 colliderOffset;

    public int taskNumber;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public void IncrementAttachments()
    {
        if (correctPartTags.Contains(attachedObject.tag))
        {
            GameManager.Instance.IncrementAttachments(taskNumber);
        }   
    }

    public void DecrementAttachments()
    {
        if (correctPartTags.Contains(attachedObject.tag))
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
        if ((XRGrabInteractableExtraAttach)args.interactableObject == null)
        {
            return;
        }

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
