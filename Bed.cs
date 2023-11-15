using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class progresses the game as soon as the bed is spawned.
 * It should be attached to the Bed Assembled prefab.
 */
public class Bed : MonoBehaviour
{
    private void Awake()
    {
        TutorialManager.Instance.PlaySpeakerClip(7);
    }
}
