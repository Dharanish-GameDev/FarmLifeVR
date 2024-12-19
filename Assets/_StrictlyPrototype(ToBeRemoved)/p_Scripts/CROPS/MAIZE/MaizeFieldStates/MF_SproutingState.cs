using FARMLIFEVR.EVENTSYSTEM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.CROPS.MAIZE
{

	public class MF_SproutingState : MaizeFeildBaseState
	{
        //Constructor
        public MF_SproutingState(MaizeFieldContext context, MaizeFieldStateMachine.EMaizeFieldState state) : base(context, state)
        {
            MaizeFieldContext maizeFieldContext = context;
        }

        #region Private Variables

        private readonly MaizeFieldStateMachine.EMaizeFieldState nextState = MaizeFieldStateMachine.EMaizeFieldState.WaterNeeded;

        #endregion

        #region Private Methods


        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=#f4bbff> Maize Field Entered Sprouting State </color>");
            EventManager.TriggerEvent(EventNames.MF_OnStateChanged,GetStateKey());

            // TODO : MAKE PLAYER GRUB THE WATER CANAL GROUND
        }
        public override void ExitState()
        {

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
            return true;//maizeFieldContext.MaizeFeildStateMachine.IsAllWaterCanalsGrubbed();
        }

        #endregion
    }
}
