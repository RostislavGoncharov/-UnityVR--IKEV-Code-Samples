using UnityEngine.InputSystem;
public interface IInteractable
{
    public void OnInteract(InputAction.CallbackContext context);
}
