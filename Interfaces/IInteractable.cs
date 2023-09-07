using UnityEngine.InputSystem;
public interface IInteractable
{
    string UIprompt { get; set; }
    public void OnInteract(InputAction.CallbackContext context);
    public void OnInteractionFinished();
    public void OnHover();
    public void OnHoverFinished();
}
