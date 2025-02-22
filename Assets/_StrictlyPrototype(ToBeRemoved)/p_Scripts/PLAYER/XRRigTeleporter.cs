using System;
using System.Threading.Tasks;
using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;

public class XRRigTeleporter : MonoBehaviour
{
    [Tooltip("The XR Rig or Player GameObject to teleport.")]
    [SerializeField] private GameObject xrRig;
    
    private CharacterController characterController;

    [SerializeField] private Transform afterMissionTeleportPoint;

    private void OnEnable()
    {
        characterController = xrRig.GetComponent<CharacterController>();
        EventManager.StartListening(EventNames.AfterMissionTeleport, AfterMissionTeleport);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventNames.AfterMissionTeleport, AfterMissionTeleport);
    }


    private async void AfterMissionTeleport()
    {
        await Task.Delay(2000);
        EventManager.TriggerEvent(EventNames.BEGIN_TeleFade);
        TeleportTo(afterMissionTeleportPoint.position, afterMissionTeleportPoint.rotation);
    }
    
    
    /// <summary>
    /// Teleports the XR Rig to the specified position and rotation.
    /// </summary>
    /// <param name="targetPosition">The world position to teleport to.</param>
    /// <param name="targetRotation">The world rotation to apply after teleportation (optional).</param>
    public void TeleportTo(Vector3 targetPosition, Quaternion? targetRotation = null)
    {
        if (xrRig == null)
        {
            Debug.LogError("XR Rig is not assigned!");
            return;
        }

        // Calculate the offset between the XR Rig's center (Camera Origin) and its camera (HMD)
        Transform cameraTransform = Camera.main.transform;
        Vector3 offset = xrRig.transform.position - cameraTransform.position;

        // Apply the offset to the target position
        Vector3 newPosition = targetPosition + offset;
        
        EventManager.TriggerEvent(EventNames.SetGravity,false);
        characterController.enabled = false;
        
        // Update the XR Rig's position
        xrRig.transform.position = newPosition;
        
        EventManager.TriggerEvent(EventNames.SetGravity,true);
        characterController.enabled = true;

        // Optionally update the XR Rig's rotation
        if (targetRotation.HasValue)
        {
            xrRig.transform.rotation = targetRotation.Value;
        }

        Debug.Log("XR Rig teleported to: " + newPosition);
    }
}
