using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using FARMLIFEVR.CATTLES.DOG;

public class DogEmoteManager : MonoBehaviour
{
    [SerializeField][Required] private GameObject emoteUIObject;
	[SerializeField][Required] private Button sitDownEmote_Btn;
	[SerializeField][Required] private Button standUpEmote_Btn;
	[SerializeField][Required] private Button dieEmote_Btn;
	[SerializeField][Required] private Button feedEmote_Btn;

    [SerializeField][Required] private DogStateMachine dogStateMachine;

    private bool isEmoteWheelEnabled = false;

    private void Start()
    {
        ValidateConstraints();
        BindButtonClickEvents();
    }

    #region Private Methods

    private void ValidateConstraints()
    {
        Assert.IsNotNull(emoteUIObject, "Emote UI Object is Null");
        Assert.IsNotNull(sitDownEmote_Btn, "SitDown Button is Null");
        Assert.IsNotNull(standUpEmote_Btn, "StandUp Button is Null");
        Assert.IsNotNull(dieEmote_Btn, "Die Button is Null");
        Assert.IsNotNull(feedEmote_Btn, "Feed Button is Null");
        Assert.IsNotNull(dogStateMachine, "Dog StateMachine is Null");
    }

    private void BindButtonClickEvents()
    {
        sitDownEmote_Btn.onClick.AddListener(() =>
        {
            dogStateMachine.DogIdle.PlayDogSitDownEmote();
        });

        standUpEmote_Btn.onClick.AddListener(() =>
        {
            dogStateMachine.DogIdle.PlayDogStandUpEmote();
        });

        dieEmote_Btn.onClick.AddListener(() =>
        {
            dogStateMachine.DogIdle.PlayDogDieEmote();
        });

        feedEmote_Btn.onClick.AddListener(() =>
        {
            dogStateMachine.DogIdle.PlayDogFeedingEmote();
        });

        emoteUIObject.SetActive(false);
    }

    #endregion


    #region Public Methods

    // It turns on and off the Emote whell when the Dog Interactable is Selected every time.
    public void ToggleEmoteWheelUI()
    {
        if (!dogStateMachine.isPlayerWithinRange) return;
        isEmoteWheelEnabled = !isEmoteWheelEnabled;
        emoteUIObject.SetActive(isEmoteWheelEnabled);
    }

    #endregion
}
