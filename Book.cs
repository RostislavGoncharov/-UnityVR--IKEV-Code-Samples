using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
* This class implements the behaviour of the book used in Task 2 in the game.
* The book uses a blinking material to draw the player's attention.
*/
public class Book : XRGrabInteractableExtraAttach, IBlinking
{
    [SerializeField] Material blinkMaterial;
    [SerializeField] GameObject bookSocketMesh;

    MeshRenderer _meshRenderer;
    List<Material> _materials = new List<Material>();

    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent<MeshRenderer>(out _meshRenderer))
        {
            Debug.Log("Book: No MeshRenderer found");
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        Blink(false);
        bookSocketMesh.SetActive(true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        Blink(true);
        bookSocketMesh.SetActive(false);
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

    public override void OnHover()
    {
        return;
    }

    public override void OnHoverFinished()
    {
        return;
    }
}
