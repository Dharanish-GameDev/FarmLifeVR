using System;
using System.Collections;
using System.Collections.Generic;
using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;

public class MissionHelper : MonoBehaviour
{
   private void OnEnable()
   {
      // Prep Events
      EventManager.StartListening(EventNames.PREP_+ EMissionType.EMT_PLOUGHING.ToString(),PrepareForPloughing);
      EventManager.StartListening(EventNames.PREP_+ EMissionType.EMT_PLANTING.ToString(),PrepareForPlanting);
      EventManager.StartListening(EventNames.PREP_+ EMissionType.EMT_GRUBBING.ToString(),PrepareForWaterCanalGrubbing);
      EventManager.StartListening(EventNames.PREP_+ EMissionType.EMT_WATERING.ToString(),PrepareForWateringPlants);
      
      
      // Conc Events
      EventManager.StartListening(EventNames.CONC_+ EMissionType.EMT_PLOUGHING.ToString(),ConcludePloughing);
      EventManager.StartListening(EventNames.CONC_+ EMissionType.EMT_PLANTING.ToString(),ConcludePlanting);
      EventManager.StartListening(EventNames.CONC_+ EMissionType.EMT_GRUBBING.ToString(),ConcludeWaterCanalGrubbing);
      EventManager.StartListening(EventNames.CONC_+ EMissionType.EMT_WATERING.ToString(),ConcludeWateringPlants);
   }

   private void OnDisable()
   {
      // Prep Events
      EventManager.StopListening(EventNames.PREP_+ EMissionType.EMT_PLOUGHING.ToString(),PrepareForPloughing);
      EventManager.StopListening(EventNames.PREP_+ EMissionType.EMT_PLANTING.ToString(),PrepareForPlanting);
      EventManager.StopListening(EventNames.PREP_+ EMissionType.EMT_GRUBBING.ToString(),PrepareForWaterCanalGrubbing);
      EventManager.StopListening(EventNames.PREP_+ EMissionType.EMT_WATERING.ToString(),PrepareForWateringPlants);
      
      // Conc Events
      EventManager.StopListening(EventNames.CONC_+ EMissionType.EMT_PLOUGHING.ToString(),ConcludePloughing);
      EventManager.StopListening(EventNames.CONC_+ EMissionType.EMT_PLANTING.ToString(),ConcludePlanting);
      EventManager.StopListening(EventNames.CONC_+ EMissionType.EMT_GRUBBING.ToString(),ConcludeWaterCanalGrubbing);
      EventManager.StopListening(EventNames.CONC_+ EMissionType.EMT_WATERING.ToString(),ConcludeWateringPlants);
   }
   
   #region Plouging
   private void PrepareForPloughing()
   {
      Debug.Log("<color=yellow>Preparing for Ploughing!</color>");
   }

   private void ConcludePloughing()
   {
      Debug.Log("<color=red>Concluding Ploughing!</color>");
   }

   #endregion

   #region Planting

   private void PrepareForPlanting()
   {
      Debug.Log("<color=yellow>Preparing for Planting!</color>");
   }

   private void ConcludePlanting()
   {
      Debug.Log("<color=red>Concluding Planting!</color>");
   }

   #endregion

   #region Grubbing

   private void PrepareForWaterCanalGrubbing()
   {
      EventManager.TriggerEvent(EventNames.MF_AdvanceToNextState);
      Debug.Log("<color=yellow>Preparing for Water Canal Grubbing!</color>");
   }

   private void ConcludeWaterCanalGrubbing()
   {
      Debug.Log("<color=red>Concluding Canal Grubbing!</color>");
   }

   #endregion

   #region Watering

   private void PrepareForWateringPlants()
   {
      EventManager.TriggerEvent(EventNames.MF_AdvanceToNextState);
      Debug.Log("<color=yellow>Preparing for Watering Plants!</color>");
   }

   private void ConcludeWateringPlants()
   {
      Debug.Log("<color=red>Concluding Watering Plants!</color>");
   }

   #endregion
}
