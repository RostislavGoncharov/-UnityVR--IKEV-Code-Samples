using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This class is a singleton which handles all key game interactions,
 * specifically when it comes to tracking tasks and counting attachments.
 */

public class GameManager : MonoBehaviour

{
    // Implement singleton functionality, engage input actions.
    public static GameManager Instance { get; private set; }
    List<TaskTracker> taskTrackerList = new List<TaskTracker>();
    [SerializeField] List<TaskText> taskTextList = new List<TaskText>();
    [SerializeField] List<Box> boxList = new List<Box>();
    [SerializeField] Transform boxSpawnPoint;
    [SerializeField] TV tv;
    [SerializeField] Image endMessage;
    [SerializeField] int tasksTotal;

    int tasksCompleted = 0;

    bool boxPickedUp = false;

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
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

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
            nextBoxIndex = rootObject.nextBoxIndex;
            tvScreenIndex = rootObject.tvScreenIndex;
        }
        
        public RootObject rootObject;

        public int tvScreenIndex;
        public int nextBoxIndex;
        public int taskNumber;
        public int attachmentsNeeded;
        public int attachmentsMade = 0;

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
                Instance.FinishTask(taskNumber);
                Debug.Log(Instance.taskTrackerList.Count);
            }
        }
    }

    public void BeginTask(RootObject rootObject, int taskNumber)
    {
        TaskTracker task = new TaskTracker(rootObject, taskNumber);

        TaskTracker existingTask = taskTrackerList.Find(x => x.taskNumber == taskNumber);

        if (existingTask != null)
        {
            task.attachmentsMade = existingTask.attachmentsMade;
            taskTrackerList.Remove(existingTask);
            Debug.Log("Task continued: " + task.taskNumber + ", attachments made: " + task.attachmentsMade);
        }

        taskTrackerList.Add(task);

        Debug.Log("Task Started: " + task.taskNumber);
    }

    public void FinishTask(int taskNumber)
    {
        TaskTracker task = taskTrackerList.Find(x => x.taskNumber == taskNumber);
        if (task != null)
        {
            if (task.nextBoxIndex != -1)
            {
                SpawnBox(task.nextBoxIndex);
            }

            if (task.tvScreenIndex != 0)
            {
                SelectTVImage(task.tvScreenIndex);
            }
            taskTrackerList.Remove(task);
            Debug.Log("Task " + task.taskNumber + " complete!");
            TaskText taskText = taskTextList.Find(x => x.taskNumber == taskNumber);
            taskText.CrossOut();
            AudioManager.Instance.PlaySoundEffect(5);

            tasksCompleted++;

            if (tasksCompleted >= tasksTotal)
            {
                ShowEndMessage();
            }
        } 
    }

    void ShowEndMessage()
    {
        endMessage.gameObject.SetActive(true);
    }

    //Logic for box spawning

    public void SpawnBox(int boxIndex)
    {
        if (boxIndex >= boxList.Count)
        {
            Debug.Log("Box index out of bounds!");
            return;
        }

        Box box = Instantiate(boxList[boxIndex], boxSpawnPoint);
        boxPickedUp = false;
        StartCoroutine(RingBell());
    }

    IEnumerator RingBell()
    {
        yield return new WaitForSeconds(2);

        while(!boxPickedUp)
        {
            AudioManager.Instance.PlaySoundEffect(3);
            yield return new WaitForSeconds(5);
        }
    }

    public void SetBoxPickedUp(bool value)
    {
        boxPickedUp = value;
    }

    public void SelectTVImage(int index)
    {
        tv.SelectImage(index);
    }
}
