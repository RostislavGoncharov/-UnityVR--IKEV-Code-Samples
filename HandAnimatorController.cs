using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private InputActionProperty pressAAction;

    private Animator anim;

    private void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();
        bool aPressedValue = pressAAction.action.ReadValue<bool>();
    }
}
