using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootObject : MonoBehaviour
{
    [SerializeField] int attachmentsNeeded;
    [SerializeField] GameObject assembledModel;

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

        GameObject[] parts = GameObject.FindGameObjectsWithTag("Part");

        foreach(GameObject part in parts)
        {
            Destroy(part);
        }

        Destroy(this.gameObject);
    }
}
