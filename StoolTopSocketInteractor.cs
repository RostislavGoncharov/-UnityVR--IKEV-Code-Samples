using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
 * This class manages interactions between two opposing stool top sockets.
 * It is needed to make sure that only one part at a time can be inserted between two stool legs.
 */
public class StoolTopSocketInteractor : ExtendedSocketInteractor
{
    [SerializeField] StoolTopSocketInteractor opposingSocket;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        /*
         * Deactivate opposing leg's socket if something is attached to the leg
         * which is attached to this socket.
         */
        base.OnSelectEntered(args);

        if (attachedObject == null || !attachedObject.CompareTag("StoolLeg"))
        {
            return;
        }

        StoolLegGrabInteractable leg = attachedObject.GetComponent<StoolLegGrabInteractable>();

        leg.onDeactivateOpposingLegSocket += SetOpposingSocketActive;

        if (!CheckIfPartIsAttachedToLeg())
        {
            return;
        }

        IXRSelectInteractable opposingLeg = opposingSocket.GetOldestInteractableSelected();

        if (opposingLeg == null || !opposingLeg.transform.CompareTag("StoolLeg"))
        {
            return;
        }

        ExtendedSocketInteractor opposingLegSocket = opposingLeg.transform.GetComponentInChildren<ExtendedSocketInteractor>();
        IXRSelectInteractable attachedPart = opposingLegSocket.GetOldestInteractableSelected();

        if (attachedPart == null)
        {
            return;
        }

        interactionManager.SelectExit(opposingLegSocket, attachedPart);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (attachedObject == null)
        {
            return;
        }

        if (attachedObject.CompareTag("StoolLeg"))
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

    bool CheckIfPartIsAttachedToLeg()
    {
        ExtendedSocketInteractor legSocket = attachedObject.GetComponentInChildren<ExtendedSocketInteractor>();
        IXRSelectInteractable attachedPart = legSocket.GetOldestInteractableSelected();

        return (attachedPart != null);
    }
}
