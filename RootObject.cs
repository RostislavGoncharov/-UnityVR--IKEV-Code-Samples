using System.Collections.Generic;
using UnityEngine;

public class RootObject : MonoBehaviour
{
    public int attachmentsNeeded;

    [SerializeField] GameObject assembledModel;
    [SerializeField] string[] partTags;
    [SerializeField] int taskNumber;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.BeginTask(this, taskNumber);
        }
    }

    private void OnEnable()
    {
        //GameManager.onSpawnModel += SpawnModel;
    }

    private void OnDisable()
    {
        //GameManager.onSpawnModel -= SpawnModel;
    }

    public void SpawnModel()
    {
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
