using UnityEngine;

// A simple script to implement falling sounds in objects. Might become part of the Audio Manager later.

[RequireComponent(typeof(AudioSource))]
public class FallingSound : MonoBehaviour
{
    [SerializeField] AudioClip sound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor") && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sound, 0.3f);
        }
    }
}
