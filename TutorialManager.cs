/*
 * This class manages the tutorial sequence at the start of the game.
 * The sequence is currently as follows:
 * 1. The player watches the Come_closer video;
 * 2. As soon as the video finishes, controllers are activated;
 * 3. The teleportation area in front of the TV is highlighted;
 * 4. Once the player has teleported closer to the TV, the Turn_speaker_on video starts;
 * 5. As soon as the speaker is turned on, background sounds appear and the first voice clip plays;
 * 6. As soon as the first clip is finished, the book starts blinking and the second clip starts playing.
 */


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [SerializeField] GameObject teleportationAreaTV;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] TV tv;
    [SerializeField] Speaker speaker;
    [SerializeField] Book book;
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
        speaker.enabled = false;
        Speaker.onHandleClipEnd += HandleClipEnd;
    }

    private void Start()
    {
        RayToggler.OnTeleport += HandleTeleportToTV;
        videoPlayer.loopPointReached += HandleVideoEnd;
    }

    private void OnDisable()
    {
        videoPlayer.loopPointReached -= HandleVideoEnd;
        Speaker.onHandleClipEnd -= HandleClipEnd;
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
        ToggleVideoPlayer(true);
        videoPlayer.Stop();
        videoPlayer.clip = videos[videoIndex];

        videoPlayer.Play();
    }

    void ToggleVideoPlayer(bool value)
    {
        videoPlayer.transform.parent.gameObject.SetActive(value);
    }

    void HandleVideoEnd(VideoPlayer vp)
    {
        if (vp.clip == videos[0])
        {
            GameManager.Instance.ToggleControllers(true);
            teleportationAreaTV.SetActive(true);
            GameManager.Instance.SetTeleportationTag(_teleportationAreaTVTag);
            ToggleVideoPlayer(false);
            tv.SelectImage(4);
        }

        if (vp.clip == videos[1])
        {
            speaker.enabled = true;
            speaker.SelectClip(0);
        }

        GameManager.Instance.MakeControllersVibrate(0.5f, 1.5f);
    }

    void HandleClipEnd(int clipIndex)
    {
        switch (clipIndex)
        {
            case 0:
                speaker.PlayClip(1);
                tv.ToggleUI(true);
                break;

            case 1:
                book.Blink(true);
                break;

            default:
                break;
        }
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
