using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FARMLIFEVR.STATEMACHINE;
using UnityEngine.Assertions;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public class MaizeFeildStateMachine : StateManager<MaizeFeildStateMachine.EMaizeFieldState>
	{
		public enum EMaizeFieldState
		{
			Seed
		}

		#region Private Variables

		//Exposed in Editor


		//Hidden
		private MaizeFieldContext maizeFieldStateContext;

		#endregion

		#region Properties



		#endregion

		#region LifeCycle Methods

		public override void Awake()
		{
			base.Awake();
			maizeFieldStateContext = new MaizeFieldContext(this);
			InitializeStates();
		}

        #endregion

        #region Private Methods

		//Initialzes all the states Relavent to the Field
		private void InitializeStates()
		{
			States.Add(EMaizeFieldState.Seed, new MaizeFieldSeedState(maizeFieldStateContext, EMaizeFieldState.Seed));
			CurrentState = States[EMaizeFieldState.Seed];
		}

        #endregion

        #region Public Methods

        //Overriden Method
        public override void ValidateConstraints() // Validating Refs
        {
			
        }
        #endregion
    }
}
