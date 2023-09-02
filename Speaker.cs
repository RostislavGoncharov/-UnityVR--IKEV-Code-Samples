using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/* This class handles the functionality of a speaker object.
 */

[RequireComponent(typeof(AudioSource))]
public class Speaker : XRSimpleInteractable, IInteractable
{
    [SerializeField] InputActionReference toggleSpeakerReference;

    AudioSource audioSource;
    bool _canBeToggled = false;

    string _uiPrompt = "A: Toggle On / Off";

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    //protected override void OnEnable()
    //{
    //    base.OnEnable();
    //    toggleSpeakerReference.action.started += onToggle;
    //}

    //protected override void OnDisable()
    //{
    //    base.OnDisable();
    //    toggleSpeakerReference.action.started -= onToggle;
    //}

    //void onToggle(InputAction.CallbackContext context)
    //{
    //    if (!_canBeToggled)
    //    {
    //        return;
    //    }

    //    if (audioSource.isPlaying)
    //    {
    //        AudioManager.Instance.PlaySoundEffect(7);
    //        audioSource.Pause();
    //    }
    //    else
    //    {
    //        AudioManager.Instance.PlaySoundEffect(7);
    //        audioSource.Play();
    //    }
    //}

    public void SetCanBeToggled (bool value)
    {
        _canBeToggled = value;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        IRay ray = args.interactorObject.transform.gameObject.GetComponent<IRay>();

        if (ray != null)
        {
            ray.ShowUIPrompt(true, _uiPrompt);
        }

        SetCanBeToggled(true);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        IRay ray = args.interactorObject.transform.gameObject.GetComponent<IRay>();

        if (ray != null)
        {
            ray.ShowUIPrompt(false);
        }

        SetCanBeToggled(false);
    }

    public void PlayClip(int index)
    {
        SelectClip(index);
        AudioManager.Instance.PlaySoundEffect(7);
        audioSource.Play();
    }

    void SelectClip(int index)
    {
        audioSource.clip = AudioManager.Instance.speakerTracks[index];
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!_canBeToggled)
        {
            return;
        }

        if (audioSource.isPlaying)
        {
            AudioManager.Instance.PlaySoundEffect(7);
            audioSource.Pause();
        }
        else
        {
            AudioManager.Instance.PlaySoundEffect(7);
            audioSource.Play();
        }
    }
}
