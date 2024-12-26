using System;
using System.Collections;
using System.Collections.Generic;
using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerLocomotionManager : MonoBehaviour
{
   [SerializeField,Required] private ActionBasedContinuousMoveProvider continuousMoveProvider;
   
   private float continuousMoveSpeed;

   private void Awake()
   {
      continuousMoveSpeed = continuousMoveProvider.moveSpeed;
      DisableMovement();
      
   }

   private void OnEnable()
   {
      EventManager.StartListening(EventNames.EnableMovement, EnableMovement);
      EventManager.StartListening(EventNames.DisableMovement, DisableMovement);
   }

   private void OnDisable()
   {
      EventManager.StopListening(EventNames.EnableMovement, EnableMovement);
      EventManager.StopListening(EventNames.DisableMovement, DisableMovement);
   }

   private void EnableMovement()
   {
      continuousMoveProvider.moveSpeed = continuousMoveSpeed;
   }

   private void DisableMovement()
   {
      continuousMoveProvider.moveSpeed = 0;
   }
}
