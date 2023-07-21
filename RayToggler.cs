/*
 * This class toggles the teleport ray on/off.
 * If the ray is pointing at the floor at the moment it's turned off,
 * the player gets teleported to the raycast hit point.
 * The sound of footsteps is played when teleportation happens.
 */

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RayToggler : MonoBehaviour
{
    public InputActionProperty teleportAction;
    public TeleportationProvider teleportationProvider;
    public float hapticAmplitude = 0.2f;
    public float hapticDuration = 0.2f;

    [SerializeField] XRRayInteractor _rayInteractor;

    XRRayInteractor _rayInteractorTeleport;

    AudioSource _audioSource;

    RaycastHit _raycastHit;
    bool _readyToTeleport;

    private void Start()
    {
        _rayInteractorTeleport = GetComponent<XRRayInteractor>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector2 teleportValue = teleportAction.action.ReadValue<Vector2>();

        if (teleportValue.y > 0)
        {
            _rayInteractorTeleport.enabled = true;
            _rayInteractor.enabled = false;
            _readyToTeleport = true;
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

        if (_rayInteractorTeleport.TryGetCurrent3DRaycastHit(out _raycastHit) && _raycastHit.transform.CompareTag("Floor"))
        {
            teleportationProvider.transform.position = _raycastHit.point;
            _rayInteractorTeleport.SendHapticImpulse(hapticAmplitude, hapticDuration);
            _audioSource.Play();
        }
    }
}
