using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
      EventManager.StartListening(EventNames.SetGravity, SetGravity);
   }

   private void OnDisable()
   {
      EventManager.StopListening(EventNames.EnableMovement, EnableMovement);
      EventManager.StopListening(EventNames.DisableMovement, DisableMovement);
      EventManager.StopListening(EventNames.SetGravity, SetGravity);
   }

   private void EnableMovement()
   {
      continuousMoveProvider.moveSpeed = continuousMoveSpeed;
   }

   private void DisableMovement()
   {
      continuousMoveProvider.moveSpeed = 0;
   }

   private void SetGravity(object[] param)
   {
      bool value = (bool)param[0];
      continuousMoveProvider.useGravity = value;
   }
}
