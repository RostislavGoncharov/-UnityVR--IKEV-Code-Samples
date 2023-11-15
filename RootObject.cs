using System.Collections.Generic;
using UnityEngine;

/*
 * This class handles the functions of a "root object"
 * which defines the number of correct attachments needed to spawn the assembled model,
 * the index of the next box to spawn after successful assembly,
 * and the task number.
 */
public class RootObject : MonoBehaviour
{
    public int attachmentsNeeded;
    public int nextBoxIndex = -1;

    [SerializeField] GameObject assembledModel;
    [SerializeField] List<string> partTags = new List<string>();
    [SerializeField] int taskNumber;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.BeginTask(this, taskNumber);
        }
    }

    public void SpawnModel()
    {
        // Spawn the assembled model, then find and destroy all relevant parts in the scene.

        Instantiate(assembledModel, transform.localPosition, transform.localRotation);

        List<GameObject> parts = new List<GameObject>();

        foreach(string tag in partTags)
        {
            parts.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }


        foreach(GameObject part in parts)
        {
            Destroy(part);
        }

        Destroy(this.gameObject);
    }
}
