using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogIdle : DogBaseState
    {
        //Constructor
        public DogIdle(DogStateContext context, DogStateMachine.EDogState state) : base(context, state)
        {
            DogStateContext dogStateContext = context;
        }

        #region Private Variables

        private bool canPerformEmote;
        private bool canStandUp;
        private bool isInGround;

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
            return dogStateContext.DogStateMachine.isPlayerWithinRange && canPerformEmote;
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
            //SetIsStandingBoolean(!isStanding);
        }

        #endregion

        public void PlayDogSitDownEmote()
        {
            if (dogStateContext.DogStateMachine.CurrentState != this) return;
            if (!CanPlayEmote()) return;
            if (isInGround) return;
            dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogSitEmote);
            canStandUp = true;
            isInGround = true;
            SetCanPerformEmoteBool(false);
            DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
        }

        public void PlayDogStandUpEmote()
        {
            if (dogStateContext.DogStateMachine.CurrentState != this) return;
            if (!CanPlayEmote()) return;
            if (!canStandUp) return;
            if (!isInGround) return;
            dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogStandUpEmote);
            SetCanPerformEmoteBool(false);
            canStandUp = false;
            isInGround = false;
            DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
        }

        public void PlayDogDieEmote()
        {
            if (dogStateContext.DogStateMachine.CurrentState != this) return;
            if (!CanPlayEmote()) return;
            if (isInGround) return;
            dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogDieEmote);
            SetCanPerformEmoteBool(false);
            canStandUp = true;
            isInGround = true;
            DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
        }

        public void PlayDogFeedingEmote()
        {
            if (dogStateContext.DogStateMachine.CurrentState != this) return;
            if (!CanPlayEmote()) return;
            if (isInGround) return;
            dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogEatEmote);
            dogStateContext.DogFoodGameObj.SetActive(true);
            SetCanPerformEmoteBool(false);
            DOVirtual.DelayedCall(2.5f, DisableDogFoodGameObject);
            DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
        }

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=cyan> Entered the Dog Idle State ! </color>");
            dogStateContext.DogAnimator.SetInteger(dogStateContext.DogAnimInt, 0);
            SetCanPerformEmoteBool(true);
            isInGround = false;
            DisableDogFoodGameObject();
        }
        public override void ExitState()
        {
            
        }
        public override void UpdateState()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2)) // SitEmote
            {
                if (dogStateContext.DogStateMachine.CurrentState != this) return;
                if (!CanPlayEmote()) return;
                if (isInGround) return;
                dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogSitEmote);
                canStandUp = true;
                isInGround = true;
                SetCanPerformEmoteBool(false);
                DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);

            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) // DogStandUp
            {
                if (dogStateContext.DogStateMachine.CurrentState != this) return;
                if (!CanPlayEmote()) return;
                if (!canStandUp) return;
                if (!isInGround) return;
                dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogStandUpEmote);
                SetCanPerformEmoteBool(false);
                canStandUp = false;
                isInGround = false;
                DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4)) // DogDie
            {
                if (dogStateContext.DogStateMachine.CurrentState != this) return;
                if (!CanPlayEmote()) return;
                if (isInGround) return;
                dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogDieEmote);
                SetCanPerformEmoteBool(false);
                canStandUp = true;
                isInGround = true;
                DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
            }
            if(Input.GetKeyDown(KeyCode.Alpha5)) // Feeding Dog
            {
                if (dogStateContext.DogStateMachine.CurrentState != this) return;
                if (!CanPlayEmote()) return;
                if (isInGround) return;
                dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogEatEmote);
                dogStateContext.DogFoodGameObj.SetActive(true);
                SetCanPerformEmoteBool(false);
                DOVirtual.DelayedCall(2.5f,DisableDogFoodGameObject);
                DOVirtual.DelayedCall(2.0f, AllowToPerformEmote);
            }
        }
        public override DogStateMachine.EDogState GetNextState()
        {
            return Statekey;
        }
        public override void OnTriggerEnterState(Collider other)
        {

        }
        public override void OnTriggerStayState(Collider other)
        {

        }
        public override void OnTriggerExitState(Collider other)
        {

        }

        #endregion
    }
}


