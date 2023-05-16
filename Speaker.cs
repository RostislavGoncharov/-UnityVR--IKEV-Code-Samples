using UnityEngine;
using UnityEngine.InputSystem;

/* This class handles the functionality of a speaker object for demo purposes.
 * It will be reworked/extended later.
 */

[RequireComponent(typeof(AudioSource))]
public class Speaker : MonoBehaviour
{
    [SerializeField] InputActionReference toggleSpeakerReference;

    AudioSource audioSource;
    bool canBeToggled = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        toggleSpeakerReference.action.started += onToggle;
    }

    private void OnDisable()
    {
        toggleSpeakerReference.action.started -= onToggle;
    }


    void onToggle(InputAction.CallbackContext context)
    {
        if (!canBeToggled)
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

    public void SetCanBeToggled (bool value)
    {
        canBeToggled = value;
    }


}
