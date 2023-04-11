using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketEvents : MonoBehaviour
{
    XRSocketInteractor socket;
    protected IXRSelectInteractable attachedObject;
    [SerializeField] string correctPartTag;

    // Start is called before the first frame update
    void Awake()
    {
        socket = gameObject.GetComponent<XRSocketInteractor>();
    }

    public virtual void Attach()
    {
        attachedObject = socket.GetOldestInteractableSelected();
    }

    public virtual void Detach()
    {

    }

    public void IncrementAttachments()
    {
        if (attachedObject.transform.CompareTag(correctPartTag))
        {
            GameManager.Instance.IncrementAttachments();
        }   
    }

    public void DecrementAttachments()
    {
        GameManager.Instance.DecrementAttachments();
    }
}
