using UnityEngine;

public class BookTaskFinished : MonoBehaviour
{
    private void OnEnable()
    {
        TutorialManager.Instance.PlaySpeakerClip(3);
    }

}
