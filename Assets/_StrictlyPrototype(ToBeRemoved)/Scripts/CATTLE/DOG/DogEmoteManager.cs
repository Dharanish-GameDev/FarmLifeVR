using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using FARMLIFEVR.EVENTSYSTEM;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogEmoteManager : MonoBehaviour
    {

        [Header("UI Elementes References")]
        [Space(5)]

        [SerializeField][Required] private GameObject emoteUIObject;
        [SerializeField][Required] private Button sitDownEmote_Btn;
        [SerializeField][Required] private Button standUpEmote_Btn;
        [SerializeField][Required] private Button dieEmote_Btn;
        [SerializeField][Required] private Button feedEmote_Btn;

        [Space(10)]
        [Header("Dog StateMachine Ref")]
        [Space(5)]
        [SerializeField][Required] private DogStateMachine dogStateMachine;

        private bool isEmoteWheelEnabled = false;

        private void Start()
        {
            ValidateConstraints();
            BindButtonsClickEvents();
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

        private void BindButtonsClickEvents()
        {
            // SitDown Emote
            sitDownEmote_Btn.onClick.AddListener(() =>
            {
                dogStateMachine.DogEmoteState.PlayDogSitDownEmote();
            });

            // Stand Up Emote
            standUpEmote_Btn.onClick.AddListener(() =>
            {
                dogStateMachine.DogEmoteState.PlayDogStandUpEmote();
            });

            // Die Emote 
            dieEmote_Btn.onClick.AddListener(() =>
            {
                dogStateMachine.DogEmoteState.PlayDogDieEmote();
            });

            // Feed Emote
            feedEmote_Btn.onClick.AddListener(() =>
            {
                dogStateMachine.DogEmoteState.PlayDogFeedingEmote();
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
            

            if (isEmoteWheelEnabled)
            {
                EventManager.TriggerEvent(EventNames.SwitchDogStateToEmote);
                emoteUIObject.SetActive(true);
            }
            else
            {
                if (!dogStateMachine.CanSwitchFromEmoteStateToOther) return;
                EventManager.TriggerEvent(EventNames.SwitchDogStateToIdle);
                emoteUIObject.SetActive(false);
            }
            
        }

        #endregion
    }
}
