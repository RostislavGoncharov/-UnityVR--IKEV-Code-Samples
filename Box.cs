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
            float offsetX = Random.Range(-0.2f, 0.2f);
            float offsetZ = Random.Range(-0.2f, 0.2f);

            Instantiate(part, transform.localPosition + new Vector3(offsetX, 1.0f, offsetZ), Quaternion.Euler(rotationX, rotationY, rotationZ));
            Debug.Log("Instantiated: " + part.name);

            Rigidbody partRigidbody = part.GetComponent<Rigidbody>();
            if (partRigidbody)
            {
                partRigidbody.AddForce(new Vector3(0, 0, 10000), ForceMode.Impulse);
                Debug.Log(partRigidbody.velocity);
            }
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
