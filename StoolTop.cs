using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
 * This class handles the stool top functionality.
 * The stool top acts as a root object for the stool,
 * can blink,
 * and triggers relevant methods of the TutorialManager when spawned.
 */

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

        TutorialManager.Instance.PlaySpeakerClip(5);
        TutorialManager.Instance.HandleBoxOpen();
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

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        Blink(false);
    }
}
