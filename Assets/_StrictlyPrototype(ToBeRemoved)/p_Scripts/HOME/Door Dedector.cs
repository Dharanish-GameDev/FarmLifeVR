using System;
using System.Collections;
using System.Collections.Generic;
using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;

public class DoorDetector : PlayerTriggerChecker
{
   public enum EDoorDedectorType
   {
      EDD_Outside,
      EDD_Inside,
   }
   
   [SerializeField] private EDoorDedectorType dedectorType;

   public override void OnPlayerTriggered()
   {
      if (dedectorType == EDoorDedectorType.EDD_Inside)
      {
         EventManager.TriggerEvent(EventNames.OnPlayerDedectedInsideOfDoor);
      }
      else if(dedectorType == EDoorDedectorType.EDD_Outside)
      {
         EventManager.TriggerEvent(EventNames.OnPlayerDedectedOutsideOfDoor);
      }
   }
}
