using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class RayToggler : XRRayInteractor
{
    XRInteractorLineVisual lineVisual;

    void OnEnabled()
    {
        lineVisual = GetComponent<XRInteractorLineVisual>();

        if (lineVisual == null)
        {
            Debug.Log("Line visual or ray interactor is null");
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!args.interactableObject.transform.CompareTag("Floor"))
        {
            lineVisual.enabled = false;
        }

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        lineVisual.enabled = true;

        base.OnSelectExited(args);
    }
}
