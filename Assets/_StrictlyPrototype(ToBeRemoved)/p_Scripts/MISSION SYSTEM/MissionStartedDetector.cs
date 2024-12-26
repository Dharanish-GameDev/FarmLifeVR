using System;
using System.Collections;
using System.Collections.Generic;
using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;

public class MissionStartedDetector : PlayerTriggerChecker
{
    public void EnableAndDisableMissionStartedDetector(bool value)
    {
        gameObject.SetActive(value);
    }

    public override void CustomAwake()
    {
        EnableAndDisableMissionStartedDetector(false);
    }

    public override void OnPlayerTriggered()
    {
        EventManager.TriggerEvent(EventNames.MissionStarted);
    }
}
