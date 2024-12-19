using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public class MF_SeedState : MaizeFeildBaseState
	{
        //Constructor
        public MF_SeedState(MaizeFieldContext context,MaizeFieldStateMachine.EMaizeFieldState state) : base(context, state) 
        {
            MaizeFieldContext maizeFieldContext = context;
        }

        #region Private Variables

        private readonly MaizeFieldStateMachine.EMaizeFieldState nextState = MaizeFieldStateMachine.EMaizeFieldState.Sprouting;

        #endregion

        #region Private Methods


        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=#f4bbff> Maize Field Entered Seed State </color>");
            EventManager.TriggerEvent(EventNames.MF_OnStateChanged,GetStateKey());
            maizeFieldContext.MaizeFieldStateMachine.MakeAllSeedsUnplanted();
            maizeFieldContext.PipeInteractable.ResetPipeInteractable();
            maizeFieldContext.PesticideSprayerInteractable.HidePesticideSprayInteractable();
            maizeFieldContext.MegaphoneInteractable.HideMegaphoneInteractable();
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
            return maizeFieldContext.MaizeFieldStateMachine.IsAllSeedsPlanted(); // If All the Seeds are Planted Give Apporoval to Change to Next State
        }

        #endregion
    }
}
