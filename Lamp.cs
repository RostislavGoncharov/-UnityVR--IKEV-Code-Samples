using UnityEngine;
using UnityEngine.InputSystem;

/*
 * This class handles player interactions with the lamp
 * (grabbing the lamp, switching the light on and off).
 */
public class Lamp : XRGrabInteractableExtraAttach
{
    [SerializeField] Light pointLight;
    [SerializeField] Light spotLight;

    protected override void Awake()
    {
        base.Awake();

        UIprompt = "Grip: Grab Lamp\nTrigger: Switch Light On/Off";

        pointLight.enabled = false;
        spotLight.enabled = false;
    }
    public override void OnInteract(InputAction.CallbackContext context)
    {
        base.OnInteract(context);

        if (context.action.name == "InteractLeft" || context.action.name == "InteractRight")
        {
            SwitchLight();
        }
    }

    void SwitchLight()
    {
        pointLight.enabled = !pointLight.enabled;
        spotLight.enabled = !spotLight.enabled;
    }
}
