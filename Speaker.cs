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

    public string UIprompt { get; set; }

    public Speaker()
    {
        UIprompt = "A: Toggle On / Off";
    }

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetCanBeToggled (bool value)
    {
        _canBeToggled = value;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        //IRay ray = args.interactorObject.transform.gameObject.GetComponent<IRay>();

        //if (ray != null)
        //{
        //    ray.ShowUIPrompt(true, );
        //}

        SetCanBeToggled(true);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        //IRay ray = args.interactorObject.transform.gameObject.GetComponent<IRay>();

        //if (ray != null)
        //{
        //    ray.ShowUIPrompt(false);
        //}

        SetCanBeToggled(false);
    }

    public void PlayClip(int index)
    {
        SelectClip(index);
        AudioManager.Instance.PlaySoundEffect(7);
        audioSource.Play();
    }

    public void SelectClip(int index)
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
            audioSource.Stop();
        }
        else
        {
            AudioManager.Instance.PlaySoundEffect(7);
            audioSource.Play();
        }

        AudioManager.Instance.ToggleEnvironmentSounds(true);
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
