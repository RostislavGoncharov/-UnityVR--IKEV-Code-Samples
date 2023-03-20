using System.Collections;
using System.Collections.Generic;
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

    // Implement logic for assembling the model. 

    public int attachmentsNeeded;

    [SerializeField] int attachmentsMade = 0;

    public delegate void SpawnModel();
    public static SpawnModel onSpawnModel;

    public void IncrementAttachments()
    {
        attachmentsMade++;
        CheckCompletion();
    }

    public void DecrementAttachments()
    {
        attachmentsMade--;
    }

    void CheckCompletion()
    {
        if (attachmentsMade == attachmentsNeeded)
        {
            onSpawnModel?.Invoke();
        }
    }


}
