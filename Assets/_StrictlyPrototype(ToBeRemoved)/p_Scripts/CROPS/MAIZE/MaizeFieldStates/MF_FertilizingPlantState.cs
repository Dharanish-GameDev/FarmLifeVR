using FARMLIFEVR.EVENTSYSTEM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public class MF_FertilizingPlantState : MaizeFeildBaseState
	{
        //Constructor
        public MF_FertilizingPlantState(MaizeFieldContext context, MaizeFieldStateMachine.EMaizeFieldState state) : base(context, state)
        {
            MaizeFieldContext maizeFieldContext = context;
        }

        #region Private Variables

        private readonly MaizeFieldStateMachine.EMaizeFieldState nextState = MaizeFieldStateMachine.EMaizeFieldState.PestControl;

        #endregion

        #region Private Methods


        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=#f4bbff> Maize Field Entered Fertilizing State </color>");
            EventManager.TriggerEvent(EventNames.MF_OnStateChanged, GetStateKey());
            maizeFieldContext.MaizeFieldStateMachine.PlanterInteractable();
        }
        public override void ExitState()
        {
            maizeFieldContext.MaizeFieldStateMachine.MakeAllPlantsUnFertilized();
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
            return maizeFieldContext.MaizeFieldStateMachine.IsAllPlantsFertilized();
        }
        #endregion
    }
}
