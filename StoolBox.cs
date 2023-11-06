using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class StoolBox : Box, IBlinking
{
    [SerializeField] Material blinkMaterial;

    MeshRenderer _meshRenderer;
    List<Material> _materials = new List<Material>();

    bool _hasVoiceoverPlayed = false;

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

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Blink(false);
        TutorialManager.Instance.MakeCarpetBlink(true);

        if (!_hasVoiceoverPlayed)
        {
            TutorialManager.Instance.PlaySpeakerClip(4);
            _hasVoiceoverPlayed = true;
        }

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        Blink(true);
        TutorialManager.Instance.MakeCarpetBlink(false);

        base.OnSelectExited(args);
    }

    protected override void OpenBox()
    {
        base.OpenBox();

        TutorialManager.Instance.stoolTop = GameObject.Find("StoolTop(Clone)").GetComponent<IBlinking>();
        TutorialManager.Instance.stoolLeg = GameObject.Find("StoolLeg(Clone)").GetComponent<IBlinking>();

        TutorialManager.Instance.MakeStoolPartsBlink();
    }
}
