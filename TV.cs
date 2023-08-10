using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Video;

/* This class handles the images and sounds coming from the TV object.
 * It also makes the TV an XRBaseInteractable, so that the player can call up the main menu
 * by interacting with the TV in the scene.
 */

public class TV : XRBaseInteractable
{
    [SerializeField] List<Sprite> images = new List<Sprite>();
    [SerializeField] List<VideoClip> videos = new List<VideoClip>();
    [SerializeField] Image screen;
    [SerializeField] VideoPlayer videoPlayer;

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

    public void PlayVideo(int videoIndex)
    {
        videoPlayer.Stop();
        videoPlayer.clip = videos[videoIndex];
        videoPlayer.Play();
    }

}
