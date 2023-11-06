using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoolBox : Box, IBlinking
{
    [SerializeField] Material blinkMaterial;

    MeshRenderer _meshRenderer;
    List<Material> _materials = new List<Material>();

    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent<MeshRenderer>(out _meshRenderer))
        {
            Debug.Log("Box: No MeshRenderer found");
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
    public override void OnInteract(InputAction.CallbackContext context)
    {
        base.OnInteract(context);

        Blink(false);
        TutorialManager.Instance.MakeCarpetBlink(true);
    }
    public override void OnInteractionFinished()
    {
        base.OnInteractionFinished();
        
        Blink(true);
        TutorialManager.Instance.MakeCarpetBlink(false);
    }

    protected override void OpenBox()
    {
        base.OpenBox();

        TutorialManager.Instance.stoolTop = GameObject.Find("StoolTop(Clone)").GetComponent<IBlinking>();
        TutorialManager.Instance.stoolLeg = GameObject.Find("StoolLeg(Clone)").GetComponent<IBlinking>();

        TutorialManager.Instance.MakeStoolPartsBlink();
    }
}
