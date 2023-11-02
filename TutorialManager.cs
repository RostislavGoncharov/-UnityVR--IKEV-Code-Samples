/*
 * This class manages the tutorials given throughout the game
 * via the speaker and TV screen.
 * It also controls various relevant object interactions
 * (such as highlighting an object when the player is supposed to interact with it).
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    public IBlinking stoolLeg;
    public IBlinking stoolTop;

    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] TV tv;
    [SerializeField] Speaker speaker;
    [SerializeField] Book book;
    [SerializeField] List<VideoClip> videos = new List<VideoClip>();
    [SerializeField] TaskText speakerTaskText;
    [SerializeField] GameObject taskList;
    [SerializeField] Carpet carpet;
    [SerializeField] GameObject plantDisassemblable;

    string _teleportationAreaFloorTag = "Floor";
    string _teleportationAreaTVTag = "TVArea";

    bool _instructionsButtonHasBeenPressed = false;


    private void Awake()
    {
        if ((Instance != null && Instance != this) /*|| GameManager.Instance.isTestEnv*/)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        book.enabled = false;
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
        ToggleVideoPlayer(false);
        StartCoroutine(WaitForVideoStart(5.0f));
    }

    public void ToggleTeleportationArea(IBlinking teleportationArea, bool value)
    {
        teleportationArea.Blink(value);
    }

    public void PlayVideo(int videoIndex)
    {
        ToggleVideoPlayer(true);
        videoPlayer.Stop();
        videoPlayer.clip = videos[videoIndex];

        videoPlayer.Play();
    }

    public void PlaySpeakerClip(int index)
    {
        speaker.PlayClip(index);
    }

    public void MakeBookBlink()
    {
        book.Blink(true);
    }

    public void MakeStoolPartsBlink()
    {       
        stoolLeg.Blink(true);
        stoolTop.Blink(true);
    }

    public void MakePlantBlink()
    {
        plantDisassemblable.GetComponent<PlantDisassemblable>().enabled = true;
    }

    public void HandleInstructionsButtonPress()
    {
        if (!_instructionsButtonHasBeenPressed)
        {
            _instructionsButtonHasBeenPressed = true;
            taskList.SetActive(true);
            PlaySpeakerClip(2);
            book.enabled = true;
            book.Blink(true);
        }
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
            //teleportationAreaTV.SetActive(true);
            ToggleTeleportationArea(carpet, true);
            carpet.transform.tag = _teleportationAreaTVTag;
            GameManager.Instance.SetTeleportationTag(_teleportationAreaTVTag);
            ToggleVideoPlayer(false);
            tv.SelectImage(4);
        }

        if (vp.clip == videos[1])
        {
            speaker.enabled = true;
            speaker.SelectClip(0);
        }

        GameManager.Instance.MakeControllersVibrate(0.5f, 1.0f);
    }

    void HandleClipEnd(int clipIndex)
    {
        switch (clipIndex)
        {
            case 0:
                PlaySpeakerClip(1);
                tv.ToggleUI(true);
                break;

            case 3:
                GameManager.Instance.SpawnBox(0);
                break;

            default:
                break;
        }
    }

    void HandleTeleportToTV(string tag)
    {
        if (tag == _teleportationAreaTVTag)
        {
            ToggleTeleportationArea(carpet, false);
            carpet.transform.tag = _teleportationAreaFloorTag;
            GameManager.Instance.SetTeleportationTag(_teleportationAreaFloorTag);

            PlayVideo(1);

            RayToggler.OnTeleport -= HandleTeleportToTV;
        }
    }

    IEnumerator WaitForVideoStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PlayVideo(0);
    }
}
