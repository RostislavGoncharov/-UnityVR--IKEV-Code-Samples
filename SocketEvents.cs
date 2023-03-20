using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketEvents : MonoBehaviour
{
    XRSocketInteractor socket;
    protected IXRSelectInteractable attachedObject;

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
        GameManager.Instance.IncrementAttachments();
    }

    public void DecrementAttachments()
    {
        GameManager.Instance.DecrementAttachments();
    }
}
