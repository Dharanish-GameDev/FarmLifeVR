using System;
using System.Collections;
using System.Collections.Generic;
using FARMLIFEVR.CROPS.MAIZE;
using FARMLIFEVR.EVENTSYSTEM;
using FARMLIFEVR.OXEN;
using FARMLIFEVR.SIMPLEINTERACTABLES;
using QFSW.QC;
using UnityEngine;

public class MissionHelper : MonoBehaviour
{
   [SerializeField,Required] private GameObject oxen;
   [SerializeField,Required] private MaizeFieldStateMachine maizeFieldStateMachine;
   [SerializeField,Required] private GameObject grubberObj;
   [SerializeField,Required] private PipeInteractable pipeInteractable;
   
   
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
      oxen.SetActive(true);
   }

   private void ConcludePloughing()
   {
      Debug.Log("<color=red>Concluding Ploughing!</color>");
      oxen.SetActive(false);
   }

   [Command]
   public void EnableOxenMover()
   {
      oxen.GetComponent<OxenMover>().enabled = true;
   }

   #endregion

   #region Planting

   private void PrepareForPlanting()
   {
      Debug.Log("<color=yellow>Preparing for Planting!</color>");
      
      maizeFieldStateMachine.PlanterInteractable();
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
      grubberObj.SetActive(true);
   }

   private void ConcludeWaterCanalGrubbing()
   {
      Debug.Log("<color=red>Concluding Canal Grubbing!</color>");
      grubberObj.SetActive(false);
   }

   #endregion

   #region Watering

   private void PrepareForWateringPlants()
   {
      EventManager.TriggerEvent(EventNames.MF_AdvanceToNextState);
      Debug.Log("<color=yellow>Preparing for Watering Plants!</color>");
      pipeInteractable.enabled = true;
   }

   private void ConcludeWateringPlants()
   {
      Debug.Log("<color=red>Concluding Watering Plants!</color>");
      pipeInteractable.enabled = false;
   }

   #endregion
}
