using FARMLIFEVR.EVENTSYSTEM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public class MF_ShoutBirdsPlantState : MaizeFeildBaseState
	{
        //Constructor
        public MF_ShoutBirdsPlantState(MaizeFieldContext context, MaizeFieldStateMachine.EMaizeFieldState state) : base(context, state)
        {
            MaizeFieldContext maizeFieldContext = context;
        }

        #region Private Variables

        private readonly MaizeFieldStateMachine.EMaizeFieldState nextState = MaizeFieldStateMachine.EMaizeFieldState.Harvesting;

        #endregion

        #region Private Methods


        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=#f4bbff> Maize Field Entered ShoutBirds State </color>");
            EventManager.TriggerEvent(EventNames.MF_OnStateChanged, GetStateKey());
            maizeFieldContext.MegaphoneInteractable.ShowMegaphoneInteractable();
        }
        public override void ExitState()
        {
            maizeFieldContext.MegaphoneInteractable.HideMegaphoneInteractable();
        }
        public override void UpdateState()
        {

        }

        public override MaizeFieldStateMachine.EMaizeFieldState GetStateKey()
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
        public override MaizeFieldStateMachine.EMaizeFieldState GetCorrespondingNextState()
        {
            return nextState;
        }
        public override bool GetHasApprovalToSwitchState()
        {
            return maizeFieldContext.MaizeFieldStateMachine.IsMaxBirdShoutCountReached();
        }
        #endregion
    }
}
