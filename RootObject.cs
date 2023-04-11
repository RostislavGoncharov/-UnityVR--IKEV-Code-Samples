using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootObject : MonoBehaviour
{
    [SerializeField] int attachmentsNeeded;
    [SerializeField] GameObject assembledModel;
    [SerializeField] string[] partTags;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.attachmentsNeeded = attachmentsNeeded;
        }
    }

    private void OnEnable()
    {
        GameManager.onSpawnModel += SpawnModel;
    }

    private void OnDisable()
    {
        GameManager.onSpawnModel -= SpawnModel;
    }

    void SpawnModel()
    {
        Instantiate(assembledModel, transform.localPosition, transform.localRotation);

        //GameObject[] parts = new GameObject[partTags.Length]; //= GameObject.FindGameObjectsWithTag(partTag);

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
