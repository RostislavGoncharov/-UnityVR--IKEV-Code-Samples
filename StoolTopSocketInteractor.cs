using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoolTopSocketInteractor : ExtendedSocketInteractor
{
    [SerializeField] StoolTopSocketInteractor opposingSocket;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (args.interactableObject.transform.CompareTag("StoolLeg"))
        {
            StoolLegGrabInteractable leg = attachedObject.GetComponent<StoolLegGrabInteractable>();

            leg.onDeactivateOpposingLegSocket += SetOpposingSocketActive;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactableObject.transform.CompareTag("StoolLeg"))
        {
            StoolLegGrabInteractable leg = attachedObject.GetComponent<StoolLegGrabInteractable>();

            leg.onDeactivateOpposingLegSocket -= SetOpposingSocketActive;
        }
    }

    void SetOpposingSocketActive(bool activeValue)
    {
        IXRSelectInteractable opposingLeg = opposingSocket.GetOldestInteractableSelected();

        if (opposingLeg != null && opposingLeg.transform.CompareTag("StoolLeg"))
        {
            ExtendedSocketInteractor opposingLegSocket = opposingLeg.transform.GetComponentInChildren<ExtendedSocketInteractor>();
            opposingLegSocket.enabled = activeValue;
        }
    }
}
