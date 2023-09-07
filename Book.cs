using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Book : MonoBehaviour, IInteractable
{
    public string UIprompt { get; set; }

    [SerializeField] Material blinkMaterial;

    MeshRenderer _meshRenderer;
    List<Material> _materials = new List<Material>();

    public Book()
    {
        UIprompt = "Grip: Grab Book";
    }

    private void Awake()
    {
        if (!TryGetComponent<MeshRenderer>(out _meshRenderer))
            {
                Debug.Log("Book: No MeshRenderer found");
            }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        // Remove the blinking material if it's present    
        _meshRenderer.GetMaterials(_materials);

        if (_materials.Count > 1)
        {
            _meshRenderer.materials = new Material[] { _materials[0] };
        }
    }

    public void Blink()
    {
        _meshRenderer.GetMaterials(_materials);
        _meshRenderer.materials = new Material[] { _materials[0], blinkMaterial };
    }

    void IInteractable.OnInteractionFinished()
    {
        return;
    }

    void IInteractable.OnHover()
    {
        Blink();
    }

    void IInteractable.OnHoverFinished()
    {
        return;
    }
}
