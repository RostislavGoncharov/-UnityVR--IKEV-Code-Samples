/*
 * This class toggles the teleport ray on/off.
 * If the ray is pointing at the floor at the moment it's turned off,
 * the player gets teleported to the raycast hit point.
 * The sound of footsteps is played when teleportation happens.
 */

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class RayToggler : MonoBehaviour
{
    public InputActionProperty teleportAction;
    public TeleportationProvider teleportationProvider;
    public float hapticAmplitude = 0.2f;
    public float hapticDuration = 0.2f;

    public delegate void Teleport(string tag);
    public static event Teleport OnTeleport;
    
    [SerializeField] XRRayInteractor _rayInteractor;
    [SerializeField] TextMeshProUGUI _promptText;

    XRRayInteractor _rayInteractorTeleport;

    RaycastHit _raycastHit;
    bool _readyToTeleport;
    Canvas _promptUI;
    string _allowedTag = "Floor";

    private void Start()
    {
        _rayInteractorTeleport = GetComponent<XRRayInteractor>();
        _promptUI = _promptText.GetComponentInParent<Canvas>();
    }

    void Update()
    {
        Vector2 teleportValue = teleportAction.action.ReadValue<Vector2>();

        if (teleportValue.y > 0)
        {
            _rayInteractorTeleport.enabled = true;
            _rayInteractor.enabled = false;

            if (_rayInteractorTeleport.TryGetCurrent3DRaycastHit(out _raycastHit) && _raycastHit.transform.CompareTag(_allowedTag))
            {
                _readyToTeleport = true;
                _promptText.text = "Release: Teleport";
                _promptUI.enabled = true;
            }
            else
            {
                _promptUI.enabled = false;
            }
        }
        else
        {
            TryTeleport();

            _rayInteractorTeleport.enabled = false;
            _rayInteractor.enabled = true;
            _readyToTeleport = false;
        }
    }

    void TryTeleport()
    {
        if (!_readyToTeleport)
        {
            return;
        }

        if (_rayInteractorTeleport.TryGetCurrent3DRaycastHit(out _raycastHit) && _raycastHit.transform.CompareTag(_allowedTag))
        {
            OnTeleport?.Invoke(_raycastHit.transform.tag);

            teleportationProvider.transform.position = _raycastHit.point;
            _rayInteractorTeleport.SendHapticImpulse(hapticAmplitude, hapticDuration);
            AudioManager.Instance.PlaySoundEffect(4);
        }

        _promptUI.enabled = false;
    }

    public void SetAllowedTag(string tag)
    {
        _allowedTag = tag;
    }
}
