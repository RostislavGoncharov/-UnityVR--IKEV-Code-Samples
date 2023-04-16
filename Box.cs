using UnityEngine;
using UnityEngine.InputSystem;

public class Box : MonoBehaviour
{
    [SerializeField] InputActionReference openBoxReference = null;
    [SerializeField] GameObject[] partsToSpawn;
    bool canBeOpened = true;

    private void OnEnable()
    {
        openBoxReference.action.started += OpenBox;
    }

    private void OnDisable()
    {
        openBoxReference.action.started -= OpenBox;
    }

    void OpenBox(InputAction.CallbackContext context)
    {
        if (canBeOpened)
        {
            SpawnParts();
        }
    }

    public void SpawnParts()
    {
        DestroyBox();

        foreach (GameObject part in partsToSpawn)
        {
            float rotationX = Random.Range(-100.0f, 100.0f);
            float rotationY = Random.Range(-100.0f, 100.0f);
            float rotationZ = Random.Range(-100.0f, 100.0f);
            Instantiate(part, transform.localPosition + new Vector3(0, 1.0f, 0), Quaternion.Euler(rotationX, rotationY, rotationZ));
            Debug.Log("Instantiated: " + part.name);
        }
    }

    void DestroyBox()
    {
        Destroy(this.gameObject);
    }

    public void SetCanBeOpened(bool mayOpen)
    {
        canBeOpened = mayOpen;
    }
}
