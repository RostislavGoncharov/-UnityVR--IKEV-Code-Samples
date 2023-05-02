using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TV : XRBaseInteractable
{
    [SerializeField] List<Sprite> images = new List<Sprite>();
    [SerializeField] Image screen;

    public void SelectImage(int imageIndex)
    {
        if (imageIndex >= images.Count)
        {
            Debug.Log("Image index out of bounds!");
            return;
        }

        screen.sprite = images[imageIndex];
        PlaySound();
    }

    public void PlaySound()
    {
        AudioSource audioSource;

        if (TryGetComponent<AudioSource>(out audioSource))
        {
            audioSource.Play();
        }
    }
}
