using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.UI;

/*
 * This class allows the UI pop-up to appear over the controller model
 * when hovering over a UI button.
 * It is necessary because IInteractable doesn't work with UI elements.
 */

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] string _uiPrompt;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData is TrackedDeviceEventData trackedDeviceEventData)
        {
            if (trackedDeviceEventData.interactor is PrimaryRay interactorRay)
            {
                interactorRay.ShowUIPrompt(true, _uiPrompt);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData is TrackedDeviceEventData trackedDeviceEventData)
        {
            if (trackedDeviceEventData.interactor is PrimaryRay interactorRay)
            {
                interactorRay.ShowUIPrompt(false);
            }
        }
    }
}
