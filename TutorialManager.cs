using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [SerializeField] GameObject teleportationAreaTV;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] List<VideoClip> videos = new List<VideoClip>();

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
    }

}
