/*
 * This class manages the tutorial sequence at the start of the game.
 * The sequence is as follows:
 * 1. The player watches the Come_closer video;
 * 2. As soon as the video finishes, controllers are activated and background sounds appear;
 * 3. The teleportation area in front of the TV is highlighted;
 * 4. Once the player has teleported closer to the TV, the Turn_speaker_on video starts;
 */


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [SerializeField] GameObject teleportationAreaTV;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] List<VideoClip> videos = new List<VideoClip>();

    string _teleportationAreaFloorTag = "Floor";
    string _teleportationAreaTVTag = "TVArea";

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

        teleportationAreaTV.SetActive(false);
    }

    private void Start()
    {
        RayToggler.OnTeleport += HandleTeleportToTV;
    }

    public void BeginTutorial()
    {
        PlayVideo(0);
    }

    public void ToggleTeleportationArea(GameObject teleportationArea, bool value)
    {
        teleportationArea.SetActive(value);
    }

    public void PlayVideo(int videoIndex)
    {
        videoPlayer.Stop();
        videoPlayer.clip = videos[videoIndex];

        if (videoIndex == 0)
        {
            videoPlayer.loopPointReached += HandleFirstVideoEnd;
        }

        else
        {
            videoPlayer.loopPointReached -= HandleFirstVideoEnd;
        }

        videoPlayer.Play();
    }

    void HandleFirstVideoEnd(VideoPlayer vp)
    {
        GameManager.Instance.ToggleControllers(true);
        GameManager.Instance.MakeControllersVibrate(0.5f, 1.5f);

        AudioManager.Instance.ToggleEnvironmentSounds(true);

        teleportationAreaTV.SetActive(true);
        GameManager.Instance.SetTeleportationTag(_teleportationAreaTVTag);


    }

    void HandleTeleportToTV(string tag)
    {
        if (tag == _teleportationAreaTVTag)
        {
            teleportationAreaTV.SetActive(false);
            GameManager.Instance.SetTeleportationTag(_teleportationAreaFloorTag);

            PlayVideo(1);

            RayToggler.OnTeleport -= HandleTeleportToTV;
        }
    }

}
