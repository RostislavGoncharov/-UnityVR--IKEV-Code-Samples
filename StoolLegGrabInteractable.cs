using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/*
 * This class handles the functionality of a Stool Leg object.
 * It is needed to turn off the socket in the opposite leg when attached to the Stool Top
 * (otherwise two parts could be attached in the same space).
 */

public class StoolLegGrabInteractable : XRGrabInteractableExtraAttach, IBlinking
{
    [SerializeField] Material blinkMaterial;

    public delegate void SetOpposingLegSocketActive(bool activeValue);
    public SetOpposingLegSocketActive onDeactivateOpposingLegSocket;

    bool _attachedToTop = false;

    MeshRenderer _meshRenderer;
    List<Material> _materials = new List<Material>();

    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent<MeshRenderer>(out _meshRenderer))
        {
            Debug.Log("Leg: No MeshRenderer found");
        }
    }

    public void OnPartInteraction(bool value)
    {
        if (_attachedToTop)
        {
            onDeactivateOpposingLegSocket?.Invoke(value);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("StoolTopSocket"))
        {
            _attachedToTop = true;
        }

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        _attachedToTop = false;

        ExtendedSocketInteractor socket = GetComponentInChildren<ExtendedSocketInteractor>();
        socket.enabled = true;

        base.OnSelectExited(args);
    }

    public override void OnInteract(InputAction.CallbackContext context)
    {
        Blink(false);
    }

    public void Blink(bool shouldBlink)
    {
        if (shouldBlink)
        {
            _meshRenderer.GetMaterials(_materials);
            _meshRenderer.materials = new Material[] { _materials[0], blinkMaterial };
        }
        else
        {
            // Remove the blinking material if it's present    
            _meshRenderer.GetMaterials(_materials);

            if (_materials.Count > 1)
            {
                _meshRenderer.materials = new Material[] { _materials[0] };
            }
        }
    }
}
