using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoolTop : XRGrabInteractableExtraAttach, IBlinking
{
    [SerializeField] Material blinkMaterial;

    MeshRenderer _meshRenderer;
    List<Material> _materials = new List<Material>();

    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent<MeshRenderer>(out _meshRenderer))
        {
            Debug.Log("Top: No MeshRenderer found");
        }
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

    public override void OnInteract(InputAction.CallbackContext context)
    {
        Blink(false);
    }
}
