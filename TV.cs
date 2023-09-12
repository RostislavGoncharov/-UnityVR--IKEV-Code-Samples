using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Video;

/* This class handles the images and sounds coming from the TV object.
 * It also inherits from XRBaseInteractable, so that the player can call up the main menu
 * by interacting with the TV in the scene. This behavior will be refined in the new demo.
 */

public class TV : XRBaseInteractable
{
    [SerializeField] List<Sprite> images = new List<Sprite>();
    [SerializeField] Image screen;
    [SerializeField] Image uiPanel;

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

    public void ToggleUI(bool value)
    {
        uiPanel.transform.gameObject.SetActive(value);
    }

}
