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

    /*
     * Separate the grab transform from the attach transform.
     * Also, set specific interaction layer mask for the part when holding it,
     * set it to default when released (unless near a suitable socket).
     * This prevents parts from sticking to sockets when thrown at them.
     */
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        SetInteractionLayerMask();

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (!canBeAttached)
        {
            SetInteractionLayerMaskToDefault();
        }

        SetAttachTransform();

        base.OnSelectExited(args);
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Hand"))
        {
            SetGrabTransform();
        }
        else
        {
            SetAttachTransform();
        }
        
        base.OnHoverEntered(args);
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
