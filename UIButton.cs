using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

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
