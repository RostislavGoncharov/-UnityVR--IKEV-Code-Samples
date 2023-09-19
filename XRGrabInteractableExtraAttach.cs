using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/*
 * This class extends the XRGrabInteractable functionality
 * by allowing you to assign separate transforms 
 * for grab and socket interactions, as well as to implement
 * any extra functionality needed.
 * 
 * It implements the IInteractable interface primarily for the UIprompt property.
 * The rest of IInteractable's functions are left empty here by default but can be used
 * to add functionality as needed.
 */

public class XRGrabInteractableExtraAttach : XRGrabInteractable, IInteractable
{
    public Transform grabTransform;
    public Transform socketAttachTransform;

    [SerializeField] InteractionLayerMask partLayers;

    public bool canBeAttached = false;

    public string UIprompt { get; set; }

    /*
     * Separate the grab transform from the attach transform.
     * Also, set specific interaction layer mask for the part when holding it,
     * set it to default when released (unless near a suitable socket).
     * This prevents parts from sticking to sockets when thrown at them.
     */
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        SetInteractionLayerMask();

        if (args.interactorObject.transform.CompareTag("Hand"))
        {
            AudioManager.Instance.PlaySoundEffect(2);

            if (GetComponent<Box>() != null)
            {
                GameManager.Instance.SetBoxPickedUp(true);
            }
        }

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
        if (args.interactorObject.transform.CompareTag("Hand") && !canBeAttached)
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

    public virtual void OnInteract(InputAction.CallbackContext context)
    {

    }

    public virtual void OnInteractionFinished()
    {

    }

    public virtual void OnHover()
    {

    }

    public virtual void OnHoverFinished()
    {

    }
}
