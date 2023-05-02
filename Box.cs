using UnityEngine;
using UnityEngine.InputSystem;

public class Box : MonoBehaviour
{
    [SerializeField] InputActionReference openBoxReference = null;
    [SerializeField] GameObject[] partsToSpawn;
    bool isOnCarpet = false;
    bool canBeOpened = false;

    static bool instructionShown = false;

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
        if (canBeOpened && isOnCarpet)
        {
            SpawnParts();

            if (!instructionShown)
            {
                GameManager.Instance.SelectTVImage(1);
                instructionShown = true;
            }
            else
            {
                GameManager.Instance.SelectTVImage(2);
            }
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
            float offsetX = Random.Range(-0.1f, 0.1f);
            float offsetZ = Random.Range(-0.1f, 0.1f);

            Instantiate(part, transform.position + new Vector3(offsetX, 0.5f, offsetZ), Quaternion.Euler(rotationX, rotationY, rotationZ));
            Debug.Log("Instantiated: " + part.name);

            // Make this work later
            Rigidbody partRigidbody = part.GetComponent<Rigidbody>();
            if (partRigidbody)
            {
                partRigidbody.AddRelativeForce(new Vector3(0, 0, 10000), ForceMode.Impulse);
                Debug.Log(partRigidbody.velocity);
            }
        }
    }

    void DestroyBox()
    {
        Destroy(this.gameObject);
    }

    public void SetCanBeOpened(bool value)
    {
        canBeOpened = value;
    }

    public void SetIsOnCarpet(bool value)
    {
        isOnCarpet = value;
    }
}
