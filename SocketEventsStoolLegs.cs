using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketEventsStoolLegs : SocketEvents
{
    public override void Attach()
    {
        base.Attach();

        attachedObject.transform.GetComponent<BoxCollider>().enabled = false;
    }
}
