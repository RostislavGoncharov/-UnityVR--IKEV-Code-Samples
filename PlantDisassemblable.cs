using System.Collections.Generic;
using UnityEngine;

public class PlantDisassemblable : DisassemblableObject, IBlinking
{
    [SerializeField] Material blinkMaterial;

    MeshRenderer _meshRenderer;
    List<Material> _materials = new List<Material>();

    private void Awake()
    {
        if (!TryGetComponent<MeshRenderer>(out _meshRenderer))
        {
            Debug.Log("Plant: No MeshRenderer found");
        }

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
}
