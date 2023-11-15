using UnityEngine;

/*
 * This class progresses the game as soon as the book is put in place.
 * This script should be attached to the Books Assembled prefab.
 */

public class BookTaskFinished : MonoBehaviour
{
    private void OnEnable()
    {
        TutorialManager.Instance.PlaySpeakerClip(3);
    }

}
