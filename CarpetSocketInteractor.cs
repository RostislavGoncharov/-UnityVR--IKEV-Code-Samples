using UnityEngine.XR.Interaction.Toolkit;

public class CarpetSocketInteractor : XRSocketInteractor
{
    int tvScreenIndex = 5;
    bool instructionShown = false;
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

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

        if (!instructionShown)
        {
            GameManager.Instance.SelectTVImage(tvScreenIndex);
            instructionShown = true;
        }        
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        IXRSelectInteractable box = args.interactableObject;
        box.transform.GetComponent<Box>().SetIsOnCarpet(true);
    }
}
