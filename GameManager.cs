using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour

{
    // Implement singleton functionality, engage input actions.
    public static GameManager Instance { get; private set; }
    List<TaskTracker> taskTrackerList = new List<TaskTracker>();

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

    /*
     * public int attachmentsNeeded;

    [SerializeField] int attachmentsMade = 0;

    public delegate void SpawnModel();
    public static SpawnModel onSpawnModel;

    void CheckCompletion()
    {
        if (attachmentsMade == attachmentsNeeded)
        {
            onSpawnModel?.Invoke();
        }
    }
    */

    public void IncrementAttachments(int taskNumber)
    {
        TaskTracker task = taskTrackerList.Find(x => x.taskNumber == taskNumber);
        if (task != null)
        {
            task.IncrementAttachments();
        }
    }

    public void DecrementAttachments(int taskNumber)
    {
        TaskTracker task = taskTrackerList.Find(x => x.taskNumber == taskNumber);
        if (task != null)
        {
            task.DecrementAttachments();
        }

    }

    /* Logic to allow multiple tasks to run in parallel.
     * A TaskTracker object handles everything related to assembly and disassembly of a particular model in the scene. 
     * It is spawned when a RootObject is instantiated.
     * The variable taskNumber is used as an id. Currently taskNumber should be set manually for each RootObject and each socket.
     * taskNumber for a RootObject and all relevant sockets should be identical.
     */
    private class TaskTracker
    {
        public TaskTracker(RootObject root, int task)
        {
            rootObject = root;
            taskNumber = task;
            attachmentsNeeded = rootObject.attachmentsNeeded;
        }
        
        public RootObject rootObject;
        public int taskNumber;
        
        public int attachmentsNeeded;
        int attachmentsMade = 0;

        public void IncrementAttachments()
        {
            attachmentsMade++;
            Debug.Log("Part attached! Attachments made: " + attachmentsMade);
            CheckCompletion();
        }

        public void DecrementAttachments()
        {
            attachmentsMade--;
            Debug.Log("Part unattached! Attachments made: " + attachmentsMade);
        }

        void CheckCompletion()
        {
            if (attachmentsMade == attachmentsNeeded)
            {
                rootObject.SpawnModel();
                Debug.Log("Model spawned");
                GameManager.Instance.FinishTask(taskNumber);
                Debug.Log(GameManager.Instance.taskTrackerList.Count);
            }
        }
    }

    public void BeginTask(RootObject rootObject, int taskNumber)
    {
        TaskTracker task = new TaskTracker(rootObject, taskNumber);
        if (task != null)
        {
            taskTrackerList.Add(task);
        }

        Debug.Log("Task Started: " + task.taskNumber);
    }

    public void FinishTask(int taskNumber)
    {
        TaskTracker task = taskTrackerList.Find(x => x.taskNumber == taskNumber);
        if (task != null)
        {
            taskTrackerList.Remove(task);
            Debug.Log("Task " + task.taskNumber + " complete!");
        } 
    }
}
