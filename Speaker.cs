using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/* This class handles the functionality of the speaker object.
 */

[RequireComponent(typeof(AudioSource))]
public class Speaker : XRSimpleInteractable, IInteractable
{
    [SerializeField] InputActionReference toggleSpeakerReference;

    AudioSource audioSource;
    bool _canBeToggled = false;

    public string UIprompt { get; set; }

    public delegate void HandleClipEnd(int clipIndex);
    public static HandleClipEnd onHandleClipEnd;

    string _turnOnPrompt = "A: Turn Speaker On";
    string _turnOffPrompt = "A: Turn Speaker Off";

    int _clipIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        UIprompt = _turnOnPrompt;
    }

    public void SetCanBeToggled (bool value)
    {
        _canBeToggled = value;
    }

    public void PlayClip(int index)
    {
        SelectClip(index);
        audioSource.Play();
        StartCoroutine(WaitForEndOfClip());
    }

    public void SelectClip(int index)
    {
        audioSource.clip = AudioManager.Instance.speakerTracks[index];
        _clipIndex = index;
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
            StopCoroutine(WaitForEndOfClip());
            UIprompt = _turnOnPrompt;
        }
        else
        {
            AudioManager.Instance.PlaySoundEffect(7);
            audioSource.Play();
            StartCoroutine(WaitForEndOfClip());
            UIprompt = _turnOffPrompt;
        }

        AudioManager.Instance.ToggleEnvironmentSounds(true);
    }

    void IInteractable.OnInteractionFinished()
    {
        return;
    }

    void IInteractable.OnHover()
    {
        if (audioSource.isPlaying)
        {
            UIprompt = _turnOffPrompt;
        }
        else
        {
            UIprompt = _turnOnPrompt;
        }

        SetCanBeToggled(true);
    }

    void IInteractable.OnHoverFinished()
    {
        SetCanBeToggled(false);
    }

    IEnumerator WaitForEndOfClip()
    {
        Debug.Log("Starting coroutine, clipIndex = " + _clipIndex);
        float _clipLength = audioSource.clip.length;
        float _timeOffset = 0.5f;

        yield return new WaitForSeconds(_clipLength + _timeOffset);
        onHandleClipEnd?.Invoke(_clipIndex);
        Debug.Log("Stopping coroutine, clipIndex = " + _clipIndex);
    }
}
