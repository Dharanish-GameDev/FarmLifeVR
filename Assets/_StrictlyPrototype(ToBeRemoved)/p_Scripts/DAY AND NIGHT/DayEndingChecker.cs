using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;

public class DayEndingChecker : PlayerTriggerChecker
{
    public override void OnPlayerTriggered()
    { 
        EventManager.TriggerEvent(EventNames.DisableMovement);
        EventManager.TriggerEvent(EventNames.MakeItNight);
        SetVisibility(false);
    }

    public override void CustomAwake()
    {
        SetVisibility(false);
    }

    public void SetVisibility(bool value)
    {
        gameObject.SetActive(value);
    }
    
    
}
