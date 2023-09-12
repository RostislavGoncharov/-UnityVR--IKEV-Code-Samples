using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
* This class implements the behaviour of the book used in Task 2 in the game.
* The book uses a blinking material to draw the player's attention.
*/
public class Book : XRGrabInteractableExtraAttach, IInteractable
{
    public string UIprompt { get; set; }

    [SerializeField] Material blinkMaterial;

    MeshRenderer _meshRenderer;
    List<Material> _materials = new List<Material>();

    public Book()
    {
        UIprompt = "Grip: Grab Book";
    }

    protected override void Awake()
    {
        base.Awake();
        
        if (!TryGetComponent<MeshRenderer>(out _meshRenderer))
            {
                Debug.Log("Book: No MeshRenderer found");
            }
    }

    public void OnInteract(InputAction.CallbackContext context)
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

    void IInteractable.OnInteractionFinished()
    {
        return;
    }

    void IInteractable.OnHover()
    {
        return;
    }

    void IInteractable.OnHoverFinished()
    {
        return;
    }
}
