using System;
using FARMLIFEVR.EVENTSYSTEM;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MissionCanvas : MonoBehaviour
{
    [Header("Text Refs")]
    [Space(3)]
    [SerializeField,Required] private TextMeshProUGUI missionText;
    [SerializeField,Required] private TextMeshProUGUI missionDescriptionText;
    [SerializeField,Required] private TextMeshProUGUI balanceMoneyText;
    
    [Space(5)]
    [Header("Button Refs")]
    [Space(3)]
    [SerializeField,Required] private Button acceptMissionButton;

    private void Awake()
    {
        ValidateConstraints();
        
        acceptMissionButton.onClick.AddListener(() =>
        {
            EventManager.TriggerEvent(EventNames.OnPlayerAcceptedMission);
        });
    }

    private void ValidateConstraints()
    {
        Assert.IsNotNull(missionText, "Mission text is null!");
        Assert.IsNotNull(missionDescriptionText, "Mission Description text is null!");
        Assert.IsNotNull(balanceMoneyText, "Balance Money text is null!");
        Assert.IsNotNull(acceptMissionButton, "Accept Button is null!");
    }
    
    #region  PublicMethods

    public void SetMissionText(string text)
    {
        missionText.SetText("Mission : " + text);
    }

    public void SetMissionDescription(string text)
    {
        missionDescriptionText.SetText(text);
    }

    public void SetBalanceMoney(int money)
    {
        balanceMoneyText.SetText(money.ToString());
    }
    
    public void SetMissionCanvasVisibility(bool value)
    {
        gameObject.SetActive(value);
    }
    
    #endregion
}
