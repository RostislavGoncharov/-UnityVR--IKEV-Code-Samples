using UnityEngine;
using TMPro;

public class TaskText : MonoBehaviour
{
    TextMeshProUGUI text;
    public int taskNumber;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        if (text == null)
        {
            Debug.Log("Text component not found");
        }
    }

    public void CrossOut()
    {
        text.fontStyle = FontStyles.Strikethrough;
    }
}
