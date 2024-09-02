using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogEmoteState : DogBaseState
    {
        public DogEmoteState(DogStateContext context, DogStateMachine.EDogState state) : base(context, state)
        {
            DogStateContext dogStateContext = context;
        }

        #region Private Variables

        private bool canPerformEmote;
        private bool canStandUp;
        private bool isInGround;

        #endregion

        #region Properties

        

        #endregion


        #region Private Methods

        // Disables the Food Mesh that appears infront of the dog while eating 
        private void DisableDogFoodGameObject()
        {
            dogStateContext.DogFoodGameObj.SetActive(false);
        }

        // It Returns is the Player is Near the Dog to do emotes
        private bool CanPlayEmote()
        {
            return dogStateContext.DogStateMachine.isPlayerWithinRange && canPerformEmote && dogStateContext.DogStateMachine.CurrentState == this;
        }

        // Sets CanPerform Emote Boolean which prohibits switching Between Emotes while playing
        private void SetCanPerformEmoteBool(bool value)
        {
            canPerformEmote = value;
        }

        //Its Allows to perform the Emote by a DoVirtual
        private void AllowToPerformEmote()
        {
            SetCanPerformEmoteBool(true);
        }

        private void SetIsInGroundBoolean(bool value)
        {
            isInGround = value;
            dogStateContext.DogStateMachine.CanSwitchFromEmoteStateToOther = !value;
        }

        #endregion

        #region Public Methods

        public void PlayDogSitDownEmote()
        {
            if (!CanPlayEmote()) return;
            if (isInGround) return;
            dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogSitEmote);
            canStandUp = true;
            SetIsInGroundBoolean(true);
            SetCanPerformEmoteBool(false);
            DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
        }

        public void PlayDogStandUpEmote()
        {
            if (!CanPlayEmote()) return;
            if (!canStandUp) return;
            if (!isInGround) return;
            dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogStandUpEmote);
            SetCanPerformEmoteBool(false);
            canStandUp = false;
            SetIsInGroundBoolean(false);
            DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
        }

        public void PlayDogDieEmote()
        {
            if (!CanPlayEmote()) return;
            if (isInGround) return;
            dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogDieEmote);
            SetCanPerformEmoteBool(false);
            canStandUp = true;
            SetIsInGroundBoolean(true);
            DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
        }

        public void PlayDogFeedingEmote()
        {
            if (!CanPlayEmote()) return;
            if (isInGround) return;
            dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogEatEmote);
            dogStateContext.DogFoodGameObj.SetActive(true);
            SetCanPerformEmoteBool(false);
            DOVirtual.DelayedCall(2.5f, DisableDogFoodGameObject);
            DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
        }

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=#f4bbff> Dog Entered Emote State </color>");

            SetCanPerformEmoteBool(true);
            isInGround = false;
            DisableDogFoodGameObject();
        }
        public override void ExitState() 
        { 
            
        }
        public override void UpdateState()
        {
            
        }

        public override DogStateMachine.EDogState GetStateKey()
        {
            return Statekey;
        }
        public override void OnTriggerEnterState(Collider other)
        {
            
        }

        public override void OnTriggerExitState(Collider other)
        {
            
        }

        public override void OnTriggerStayState(Collider other)
        {
           
        }
        #endregion
    }
}

