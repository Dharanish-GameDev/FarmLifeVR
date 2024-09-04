using FARMLIFEVR.EVENTSYSTEM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public class MF_MaturePlantState : MaizeFeildBaseState
	{
        //Constructor
        public MF_MaturePlantState(MaizeFieldContext context, MaizeFeildStateMachine.EMaizeFieldState state) : base(context, state)
        {
            MaizeFieldContext maizeFieldContext = context;
        }

        #region Private Variables

        private readonly MaizeFeildStateMachine.EMaizeFieldState nextState = MaizeFeildStateMachine.EMaizeFieldState.Harvesting;

        #endregion

        #region Private Methods


        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=#f4bbff> Maize Field Entered MaturePlantState State </color>");
            EventManager.TriggerEvent(EventNames.MF_OnStateChanged, GetStateKey());
        }
        public override void ExitState()
        {

        }
        public override void UpdateState()
        {

        }

        public override MaizeFeildStateMachine.EMaizeFieldState GetStateKey()
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
        public override MaizeFeildStateMachine.EMaizeFieldState GetCorrespondingNextState()
        {
            return nextState;
        }
        public override bool GetHasApprovalToSwitchNextState()
        {
            return true;
        }
        #endregion
    }
}
