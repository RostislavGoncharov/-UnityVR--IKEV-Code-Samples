using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/*
 * This class defines the behavior of the ray used for any player interactions
 * (except for teleporting).
 * It updates the UI prompt which appears over the controller when an interaction is possible.
 * It also triggers any behaviour the interactable object might have. 
 */

public class PrimaryRay : XRRayInteractor, IRay
{
    [SerializeField] TextMeshProUGUI _promptText;
    [SerializeField] InputActionReference _interactActionReference;

    Canvas _promptUI;

    protected override void Start()
    {
        base.Start();
        _promptUI = _promptText.GetComponentInParent<Canvas>();
    }
    public void ShowUIPrompt(bool isVisible, string text = "")
    {
        _promptUI.enabled = isVisible;
        _promptText.text = text;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        IInteractable interactable = args.interactableObject.transform.GetComponent<IInteractable>();

        if (interactable != null)
        {
            _interactActionReference.action.started += interactable.OnInteract;
            ShowUIPrompt(true, interactable.UIprompt);

            interactable.OnHover();
        }
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        IInteractable interactable = args.interactableObject.transform.GetComponent<IInteractable>();

        if (interactable != null)
        {
            _interactActionReference.action.started -= interactable.OnInteract;
            ShowUIPrompt(false);

            interactable.OnHoverFinished();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        IInteractable interactable = args.interactableObject.transform.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.OnInteract(new InputAction.CallbackContext());
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        IInteractable interactable = args.interactableObject.transform.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.OnInteractionFinished();
        }
    }
}
