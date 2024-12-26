using System.Collections;
using System.Collections.Generic;
using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MissionSO", menuName = "Mission System/MissionSO")]
public class MissionSO : ScriptableObject
{
    public string missionName;
    public string missionDescription;
    [FormerlySerializedAs("prepEventType")] public EMissionType missionTypeType;
}
