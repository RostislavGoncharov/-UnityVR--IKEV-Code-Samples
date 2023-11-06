using UnityEngine.InputSystem;

/*
 * This interface declares the behavior of all interactable objects.
 * The UIprompt property is used to update the prompt appearing over the controller
 * when an interaction is available.
 */
public interface IInteractable
{
    string UIprompt { get; set; }
    public void OnInteract(InputAction.CallbackContext context);
    public void OnInteractionFinished();
    public void OnHover();
    public void OnHoverFinished();
}
