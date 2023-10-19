using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

/*
 * This class handles all functionality of a box with furniture parts:
 * opening the box, spawning the parts, destroying the box afterwards.
 */
public class Box : XRGrabInteractableExtraAttach
{
    [SerializeField] List<GameObject> partsToSpawn;
    [SerializeField] InputAction openBoxAction;

    bool _isOnCarpet = false;
    bool _canBeOpened = false;

    string _canGrabPrompt = "Grip: Grab Box";
    string _canOpenPrompt = "Grip: Grab Box\nTrigger: Open Box";

    protected override void OnEnable()
    {
        base.OnEnable();
        
        UIprompt = _canGrabPrompt;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        SetCanBeOpened(true);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        SetCanBeOpened(false);
    }

    private void Update()
    {
        if (_isOnCarpet)
        {
            UIprompt = _canOpenPrompt;
        }
        else
        {
            UIprompt = _canGrabPrompt;
        }
    }

    void OpenBox()
    {
        if (_canBeOpened && _isOnCarpet)
        {
            SpawnParts();
        }
    }

    public void SpawnParts()
    {
        DestroyBox();

        foreach (GameObject part in partsToSpawn)
        {
            float rotationX = Random.Range(-100.0f, 100.0f);
            float rotationY = Random.Range(-100.0f, 100.0f);
            float rotationZ = Random.Range(-100.0f, 100.0f);
            float offsetX = Random.Range(-0.1f, 0.1f);
            float offsetZ = Random.Range(-0.1f, 0.1f);

            Instantiate(part, transform.position + new Vector3(offsetX, 0.5f, offsetZ), Quaternion.Euler(rotationX, rotationY, rotationZ));
            Debug.Log("Instantiated: " + part.name);
        }
    }

    public void SetCanBeOpened(bool value)
    {
        _canBeOpened = value;
    }

    public void SetIsOnCarpet(bool value)
    {
        _isOnCarpet = value;
    }

    public override void OnInteract(InputAction.CallbackContext context)
    {
        base.OnInteract(context);

        if (context.action.name == "InteractLeft" || context.action.name == "InteractRight")
        {
            OpenBox();
        }
    }
    void DestroyBox()
    {
        Destroy(this.gameObject);
    }
}
