using DG.Tweening;
using FARMLIFEVR.EVENTSYSTEM;
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

        private bool canStandUp;

        #endregion


        #region Private Methods

        // Sets CanStandUp to true which allows standing up after sitting down or died
        private void AllowStandUpEmote()
        {
            SetCanStandUpBool(true);
        }

        // Disables the Food Mesh that appears infront of the dog while eating 
        private void DisableDogFoodGameObject()
        {
            dogStateContext.DogFoodGameObj.SetActive(false);
        }

        // It Returns is the Player is Near the Dog to do emotes
        private bool IsPlayerWithinRange()
        {
            return dogStateContext.DogOwnerOverLap.GetIsOverlapping();
        }

        // Sets the CanStandUpBool that control emote states
        private void SetCanStandUpBool(bool value)
        {
            canStandUp = value;
        }

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=cyan> Entered the Dog Idle State ! </color>");
            dogStateContext.DogAnimator.SetInteger(dogStateContext.DogAnimInt, 0);
            canStandUp = false;
            DisableDogFoodGameObject();
        }
        public override void ExitState()
        {
            
        }
        public override void UpdateState()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2)) // SitEmote
            {
                if (!IsPlayerWithinRange()) return;
                SetCanStandUpBool(false);
                dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogSitEmote);
                DOVirtual.DelayedCall(2.0f, AllowStandUpEmote);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) // DogStandUp
            {
                if (!IsPlayerWithinRange()) return;
                if (!canStandUp) return;
                dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogStandUpEmote);
                SetCanStandUpBool(false);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4)) // DogDie
            {
                if (!IsPlayerWithinRange()) return;
                dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogDieEmote);
                DOVirtual.DelayedCall(2.0f, AllowStandUpEmote);
            }
            if(Input.GetKeyDown(KeyCode.Alpha5)) // Feeding Dog
            {
                if (!IsPlayerWithinRange()) return;
                dogStateContext.DogAnimator.SetTrigger(dogStateContext.DogEatEmote);
                dogStateContext.DogFoodGameObj.SetActive(true);
                DOVirtual.DelayedCall(2.5f,DisableDogFoodGameObject);
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


