using UnityEngine;
using TMPro;

// This class allows the GameManager to cross out the completed tasks from the list.

public class TaskText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int taskNumber;

    [SerializeField] bool _startsCrossedOut;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        if (text == null)
        {
            Debug.Log("Text component not found");
        }

        if (_startsCrossedOut)
        {
            CrossOut();
        }
    }

    public void CrossOut()
    {
        Debug.Log("Crossed out");
        text.fontStyle = FontStyles.Strikethrough;
    }
}
