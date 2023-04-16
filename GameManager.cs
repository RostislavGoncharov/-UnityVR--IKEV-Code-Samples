using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour

{
    // Implement singleton functionality, engage input actions.
    public static GameManager Instance { get; private set; }

    DebugUIControls debugUIControls;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        // Temporary Quit functionality.
        debugUIControls = new DebugUIControls();
        debugUIControls.Debug.Quit.performed += OnQuitGame;
    }

    private void OnQuitGame(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        debugUIControls.Debug.Enable();
    }

    private void OnDisable()
    {
        debugUIControls.Debug.Disable();
    }

    // Logic for assembling the model. 

    public int attachmentsNeeded;

    [SerializeField] int attachmentsMade = 0;

    public delegate void SpawnModel();
    public static SpawnModel onSpawnModel;

    public void IncrementAttachments()
    {
        attachmentsMade++;
        Debug.Log("Attached! Attachments: " + attachmentsMade);
        CheckCompletion();
    }

    public void DecrementAttachments()
    {
        attachmentsMade--;
        Debug.Log("Unattached! Attachments: " + attachmentsMade);
    }

    void CheckCompletion()
    {
        if (attachmentsMade == attachmentsNeeded)
        {
            onSpawnModel?.Invoke();
        }
    }

    // Logic for spawning and a box.


}
