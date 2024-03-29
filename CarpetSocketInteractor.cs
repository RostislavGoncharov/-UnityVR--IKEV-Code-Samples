using UnityEngine.XR.Interaction.Toolkit;

// This class handles the interactions between the box and the carpet.

public class CarpetSocketInteractor : XRSocketInteractor
{
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        // Automatically attach the box to the carpet when in range
        XRGrabInteractableExtraAttach box = (XRGrabInteractableExtraAttach)args.interactableObject;
        box.canBeAttached = true;
        interactionManager.SelectEnter(this, (IXRSelectInteractable)box);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        XRGrabInteractableExtraAttach box = (XRGrabInteractableExtraAttach)args.interactableObject;
        box.canBeAttached = false;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        IXRSelectInteractable box = args.interactableObject;
        box.transform.GetComponent<Box>().SetIsOnCarpet(true);       
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        IXRSelectInteractable box = args.interactableObject;
        box.transform.GetComponent<Box>().SetIsOnCarpet(true);
    }
}
