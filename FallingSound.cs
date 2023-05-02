using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
