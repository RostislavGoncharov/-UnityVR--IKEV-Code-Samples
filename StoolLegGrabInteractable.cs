using UnityEngine.XR.Interaction.Toolkit;

/*
 * This class handles the functionality of a Stool Leg object.
 * It is needed to turn off the socket in the opposite leg when attached to the Stool Top
 * (otherwise two parts could be attached in the same space).
 */

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
