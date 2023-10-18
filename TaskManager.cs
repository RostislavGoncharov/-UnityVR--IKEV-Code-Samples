using System.Collections.Generic;
using UnityEngine;

/*
 * This class handles everything related to task management:
 * beginning and finishing tasks, counting attachments, and checking if an assembled model needs to be spawned.
 */

public class TaskManager : MonoBehaviour
{
    [SerializeField] List<Task> taskList = new List<Task>();
    [SerializeField] int tasksCompleted = 0;

    public int tasksTotal;
    public List<TaskText> taskTextList = new List<TaskText>();

    public void BeginTask(RootObject rootObject, int taskNumber)
    {
        GameObject taskObject = new GameObject("Task " + taskNumber);
        Task task = taskObject.AddComponent<Task>();
        task.rootObject = rootObject;
        task.taskNumber = taskNumber;
        task.attachmentsNeeded = rootObject.attachmentsNeeded;
        task.nextBoxIndex = rootObject.nextBoxIndex;

        Task existingTask = taskList.Find(x => x.taskNumber == taskNumber);

        if (existingTask != null)
        {
            task.attachmentsMade = existingTask.attachmentsMade;
            taskList.Remove(existingTask);
            Debug.Log("Task continued: " + task.taskNumber + ", attachments made: " + task.attachmentsMade);
        }

        taskList.Add(task);

        Debug.Log("Task Started: " + task.taskNumber);
    }

    public void FinishTask(int taskNumber)
    {
        Task task = null;
        bool taskIsInList = CheckIfTaskInList(taskNumber, out task);
        if (taskIsInList)
        {
            if (task.nextBoxIndex != -1)
            {
                GameManager.Instance.SpawnBox(task.nextBoxIndex);
            }

            //if (task.tvScreenIndex != 0)
            //{
            //    GameManager.Instance.SelectTVImage(task.tvScreenIndex);
            //}
            TaskText taskText = taskTextList.Find(x => x.taskNumber == taskNumber);
            taskText.CrossOut();
            AudioManager.Instance.PlaySoundEffect(5);
            taskList.Remove(task);
            Debug.Log("Task " + task.taskNumber + " complete!");

            tasksCompleted++;

            if (tasksCompleted >= tasksTotal)
            {
                GameManager.Instance.ShowEndMessage();
            }
        }
    }

    public bool CheckIfTaskInList(int taskNumber, out Task task)
    {
        task = taskList.Find(x => x.taskNumber == taskNumber);

        return task != null;
    }

    public void IncrementAttachments(int taskNumber)
    {
        Task task;
        bool taskIsInList = CheckIfTaskInList(taskNumber, out task);

        if (taskIsInList)
        {
            Debug.Log("incrementing");
            task.attachmentsMade++;
            Debug.Log("Part attached! Attachments made: " + task.attachmentsMade);
            CheckCompletion(task);
        }
    }

    public void DecrementAttachments(int taskNumber)
    {
        Task task = null;
        bool taskIsInList = CheckIfTaskInList(taskNumber, out task);

        if (taskIsInList)
        {
            task.attachmentsMade--;
            Debug.Log("Part unattached! Attachments made: " + task.attachmentsMade);
        }
    }

    void CheckCompletion(Task task)
    {
        if (task.attachmentsMade == task.attachmentsNeeded)
        {
            FinishTask(task.taskNumber);
            task.rootObject.SpawnModel();
            Debug.Log("Model spawned");
        }
    }
}
