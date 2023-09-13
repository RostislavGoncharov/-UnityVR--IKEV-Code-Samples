using UnityEngine;

/* 
 * This class contains all data related to assembly and disassembly of a particular model in the scene. 
 * It is spawned when a RootObject is instantiated.
 * Currently _taskId should be set manually in the inspector for each RootObject and each socket.
 * _taskId for a RootObject and all relevant sockets should be identical.
 */
public class Task : MonoBehaviour
{
    public Task(RootObject root, int _taskId)
    {
        rootObject = root;
        taskNumber = _taskId;
        attachmentsNeeded = rootObject.attachmentsNeeded;
        attachmentsMade = 0;
        nextBoxIndex = rootObject.nextBoxIndex;
        //tvScreenIndex = rootObject.tvScreenIndex;
    }

    public RootObject rootObject;

    //public int tvScreenIndex;
    public int nextBoxIndex;
    public int taskNumber;
    public int attachmentsNeeded;
    public int attachmentsMade;
}
