using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;
using DG.Tweening;

public class DoorOpener : MonoBehaviour
{
   public enum EDoorStates
   {
      Closed,
      OpeningOutwards,
      OpeningInwards,
      Opened
   }
   
   [Header("Door Transform refs")]
   [Space(3)]
   [SerializeField,Required] private Transform leftDoor;
   [SerializeField,Required] private Transform rightDoor;

   [Space(5)] 
   [Header("Door Rotation Values")]
   [Space(3)]
   
   [SerializeField] private float doorRotationDuration;
   
   [Space(3)]
   
   [SerializeField] private float lOutwardAngle;
   [SerializeField] private float rOutwardAngle;
   
   [Space(3)]
   
   [SerializeField] private float lInwardAngle;
   [SerializeField] private float rInwardAngle;
   
   
    private EDoorStates doorState = EDoorStates.Closed;

   private void OnEnable()
   {
      EventManager.StartListening(EventNames.OnPlayerDedectedInsideOfDoor, OnPlayerDedectedInsideOfDoor);
      EventManager.StartListening(EventNames.OnPlayerDedectedOutsideOfDoor, OnPlayerDedectedOutsideOfDoor);
   }

   private void OnDisable()
   {
      EventManager.StopListening(EventNames.OnPlayerDedectedInsideOfDoor, OnPlayerDedectedInsideOfDoor);
      EventManager.StopListening(EventNames.OnPlayerDedectedOutsideOfDoor, OnPlayerDedectedOutsideOfDoor);
   }

   private void OnPlayerDedectedInsideOfDoor()
   {
      if (doorState == EDoorStates.Closed)
      {
         SetDoorState(EDoorStates.OpeningOutwards);
         return;
      }
      SetDoorState(EDoorStates.Closed);
   }
   private void OnPlayerDedectedOutsideOfDoor()
   {
     // Debug.Log("Player Dedected Outside of Door");
      if (doorState == EDoorStates.Closed)
      {
         SetDoorState(EDoorStates.OpeningInwards);
         return;
      }
      SetDoorState(EDoorStates.Closed);
     
      
   }

   private void SetDoorState(EDoorStates newDoorState)
   {
      doorState = newDoorState;

      switch (doorState)
      {
         case EDoorStates.Closed:
            CloseDoors();
            break;

         case EDoorStates.OpeningOutwards:
            OpenDoorsOutwards();
            break;

         case EDoorStates.OpeningInwards:
            OpenDoorsInwards();
            break;

         case EDoorStates.Opened:
            OpenDoorsOutwards();
            break;
      }
   }

   private void OpenDoorsOutwards()
   {
      // Rotate the doors to the outward angle
      leftDoor.DORotate(new Vector3(0, lOutwardAngle, 0), doorRotationDuration).OnComplete(()=>doorState = EDoorStates.Opened);
      rightDoor.DORotate(new Vector3(0, rOutwardAngle, 0), doorRotationDuration);
   }

   private void OpenDoorsInwards()
   {
      // Rotate the doors to the inward angle
      leftDoor.DORotate(new Vector3(0, lInwardAngle, 0), doorRotationDuration).OnComplete(()=>doorState = EDoorStates.Opened);
      rightDoor.DORotate(new Vector3(0, rInwardAngle, 0), doorRotationDuration);
   }

   private void CloseDoors()
   {
      // Close the doors (rotate to 0 on Y-axis)
      leftDoor.DORotate(Vector3.zero, doorRotationDuration).OnComplete(()=>doorState = EDoorStates.Closed);
      rightDoor.DORotate(Vector3.zero, doorRotationDuration);
   }
}
