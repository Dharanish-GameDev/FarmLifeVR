using UnityEngine;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public class MaizeFieldSeedState : MaizeFeildBaseState
	{
        //Constructor
        public MaizeFieldSeedState(MaizeFieldContext context,MaizeFeildStateMachine.EMaizeFieldState state) : base(context, state) 
        {
            MaizeFieldContext maizeFieldContext = context;
            //Statekey = state;
        }

        #region Private Variables


        #endregion

        #region Private Methods


        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=#f4bbff> Maize Field Entered Seed State </color>");
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

        #endregion
    }
}
