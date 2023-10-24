using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlantDisassemblable : DisassemblableObject, IBlinking, IInteractable
{
    [SerializeField] Material blinkMaterial;
    [SerializeField] MeshRenderer _meshRenderer;

    List<Material> _materials = new List<Material>();

    public string UIprompt { get; set; }

    protected override void Awake()
    {
        base.Awake();

        UIprompt = "Trigger: Disassemble Plant";
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Blink(true);
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        Disassemble();
    }

    public void OnInteractionFinished()
    {
        return;
    }

    public void OnHover()
    {
        return;
    }

    public void OnHoverFinished()
    {
        return;
    }
}
