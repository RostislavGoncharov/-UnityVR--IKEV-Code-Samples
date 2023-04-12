using UnityEngine;
using UnityEngine.InputSystem;

public class Box : MonoBehaviour
{
    [SerializeField] InputActionReference openBoxReference = null;
    [SerializeField] GameObject partsToSpawn;
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
        Instantiate(partsToSpawn, transform.position, Quaternion.identity);

        DestroyBox();
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
