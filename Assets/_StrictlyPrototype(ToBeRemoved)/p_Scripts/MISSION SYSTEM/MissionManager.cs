using System;
using System.Collections;
using System.Collections.Generic;
using FARMLIFEVR.EVENTSYSTEM;
using QFSW.QC;
using UnityEngine;
using UnityEngine.Assertions;

public class MissionManager : MonoBehaviour
{
    [SerializeField,Required] private MissionCanvas missionCanvas;
    [SerializeField,Required] private MissionStartedDetector missionStartedDetector;
    [SerializeField,Required] private DayEndingChecker dayEndingChecker;
    [SerializeField] private List<MissionSO> missionsList;
    private int missionIndex = 0;

    private void Awake()
    {
        ValidateConstraints();
        UpdateMissionUIText();
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventNames.AdvanceToNextMission, AdvanceToNextMission);
        EventManager.StartListening(EventNames.OnPlayerAcceptedMission,OnPlayerAcceptedMission);
        EventManager.StartListening(EventNames.MissionStarted,OnMissionStartedByPlayer);
        EventManager.StartListening(EventNames.MissionCompleted,OnMissionCompletedByPlayer);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventNames.AdvanceToNextMission, AdvanceToNextMission);
        EventManager.StopListening(EventNames.OnPlayerAcceptedMission,OnPlayerAcceptedMission);
        EventManager.StopListening(EventNames.MissionStarted,OnMissionStartedByPlayer);
        EventManager.StopListening(EventNames.MissionCompleted,OnMissionCompletedByPlayer);
    }

    #region PrivateMethods

    private void ValidateConstraints()
    {
        Assert.IsNotNull(missionCanvas, "Mission Canvas is Null!");  
    }

    private void UpdateMissionUIText()
    {
        missionCanvas.SetMissionText(GetCurrentMissionName());
        missionCanvas.SetMissionDescription(GetCurrentMissionDescription());
    }

    private string GetCurrentMissionName()
    {
        if (missionIndex < 0 || missionIndex >= missionsList.Count)
        {
            return "Invalid mission index";
        }
        return missionsList[missionIndex].missionName;
    }

    private string GetCurrentMissionDescription()
    {
        if (missionIndex < 0 || missionIndex >= missionsList.Count)
        {
            return "Invalid mission index";
        }
        return missionsList[missionIndex].missionDescription;
    }

    private void OnPlayerAcceptedMission()
    {
        missionStartedDetector.EnableAndDisableMissionStartedDetector(true);
        missionCanvas.SetMissionCanvasVisibility(false);
        EventManager.TriggerEvent(GetCurrentPrepEventName());
        EventManager.TriggerEvent(EventNames.EnableMovement);
    }

    private string GetCurrentPrepEventName()
    {
        if (missionIndex < 0 || missionIndex >= missionsList.Count)
        {
            return "Invalid mission index";
        }
        return EventNames.PREP_+ missionsList[missionIndex].missionTypeType.ToString();
    }
    
    private string GetCurrentConcEventName()
    {
        if (missionIndex < 0 || missionIndex >= missionsList.Count)
        {
            return "Invalid mission index";
        }
        return EventNames.CONC_+ missionsList[missionIndex].missionTypeType.ToString();
    }

    
    private void AdvanceToNextMission()
    {
        missionIndex++;
        missionCanvas.SetMissionCanvasVisibility(true);
        UpdateMissionUIText();
    }
    #endregion


    #region PublicMethods

    

    public void OnMissionStartedByPlayer()
    {
        missionStartedDetector.EnableAndDisableMissionStartedDetector(false);
        EventManager.TriggerEvent(EventNames.MakeItDay);
        Debug.Log("<color=green>OnMissionStartedByPlayer</color>");
    }

    [Command]
    public void OnMissionCompletedByPlayer()
    {
        Debug.Log("<color=magenta>OnMissionCompletedByPlayer</color>");
        EventManager.TriggerEvent(EventNames.MakeItEvening);
        dayEndingChecker.SetVisibility(true);
        EventManager.TriggerEvent(GetCurrentConcEventName());
    }

    #endregion
}
