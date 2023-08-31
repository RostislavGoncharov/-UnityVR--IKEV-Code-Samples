using System.Collections.Generic;
using UnityEngine;

// This class is responsible for playing all 2D one-shot sound effects

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] List<AudioClip> soundEffects = new List<AudioClip>();
    [SerializeField] List<AudioSource> environmentSounds = new List<AudioSource>();

    public List<AudioClip> speakerTracks = new List<AudioClip>();

    AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(int index)
    {
        audioSource.PlayOneShot(soundEffects[index]);
    }

    public void ToggleEnvironmentSounds(bool value)
    {
        foreach (AudioSource sound in environmentSounds)
        {
            sound.enabled = value;
        }
    }
}
