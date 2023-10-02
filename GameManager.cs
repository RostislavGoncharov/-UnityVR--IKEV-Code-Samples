using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

/*
 * This class is a singleton which handles all key game interactions 
 * and facilitates story progression.
 */

public class GameManager : MonoBehaviour

{
    public static GameManager Instance { get; private set; }

    [SerializeField] List<TaskText> taskTextList = new List<TaskText>();
    [SerializeField] List<Box> boxList = new List<Box>();
    [SerializeField] Transform boxSpawnPoint;
    [SerializeField] TV tv;
    [SerializeField] GameObject controllers;
    [SerializeField] Image endMessage;
    [SerializeField] int tasksTotal;

    //public bool isTestEnv = false;

    TaskManager _taskManager;

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

        //if (!isTestEnv)
        {
            ToggleControllers(false);
            AudioManager.Instance.ToggleEnvironmentSounds(true);
        }

        GameObject _taskManagerObject = new GameObject("Task Manager");
        _taskManager = _taskManagerObject.AddComponent<TaskManager>();
        _taskManager.tasksTotal = tasksTotal;
        _taskManager.taskTextList = taskTextList;
    }

    private void Start()
    {
        //if (!isTestEnv)
        //{
        //    TutorialManager.Instance.BeginTutorial();
        //}
        //else
        //{
        //    tv.ToggleUI(true);
        //}

        TutorialManager.Instance.BeginTutorial();
    }

    public void ToggleControllers(bool value)
    {
        controllers.SetActive(value);
    }

    public void MakeControllersVibrate(float amplitude, float duration)
    {
        ActionBasedController[] _controllers = controllers.GetComponentsInChildren<ActionBasedController>();

        foreach (ActionBasedController controller in _controllers)
        {
            controller.SendHapticImpulse(amplitude, duration);
        }
    }

    public void SetTeleportationTag(string tag)
    {
        RayToggler[] rays = controllers.GetComponentsInChildren<RayToggler>();

        foreach (RayToggler ray in rays)
        {
            ray.SetAllowedTag(tag);
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
        _taskManager.IncrementAttachments(taskNumber);
    }

    public void DecrementAttachments(int taskNumber)
    {
        _taskManager.DecrementAttachments(taskNumber);
    }

    public void BeginTask(RootObject rootObject, int taskNumber)
    {
        _taskManager.BeginTask(rootObject, taskNumber);
    }

    public void FinishTask(int taskNumber)
    {
        _taskManager.FinishTask(taskNumber);
    }

    public void ShowEndMessage()
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

    public GameObject GetControllers()
    {
        return controllers;
    }
}
