using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FARMLIFEVR.STATEMACHINE;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public class MaizeFeildStateMachine : StateManager<MaizeFeildStateMachine.EMaizeFieldState>
	{
		public enum EMaizeFieldState
		{
			Seed,
			Sprouting,
			WaterNeeded,
			SmallPlant,
			MediumPlant,
			PestControl,
			MaturePlant,
			Harvesting
		}

		#region Private Variables

		//Exposed in Editor


		//Hidden
		private MaizeFieldContext maizeFieldStateContext;
		private MaizeFeildBaseState currentMaizeFieldState;

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

		public override void Update()
		{
			base.Update();
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                TransitionToNextState();
            }
        }
        #endregion

        #region Private Methods

        //Initialzes all the states Relavent to the Field
        private void InitializeStates()
		{
			States.Add(EMaizeFieldState.Seed, new MF_SeedState(maizeFieldStateContext, EMaizeFieldState.Seed));
			States.Add(EMaizeFieldState.Sprouting, new MF_SproutingState(maizeFieldStateContext, EMaizeFieldState.Sprouting));
			States.Add(EMaizeFieldState.WaterNeeded, new MF_WaterNeededState(maizeFieldStateContext, EMaizeFieldState.WaterNeeded));
			States.Add(EMaizeFieldState.SmallPlant, new MF_SmallPlantState(maizeFieldStateContext, EMaizeFieldState.SmallPlant));
			States.Add(EMaizeFieldState.MediumPlant, new MF_MediumPlantState(maizeFieldStateContext, EMaizeFieldState.MediumPlant));
			States.Add(EMaizeFieldState.PestControl, new MF_PestControlState(maizeFieldStateContext, EMaizeFieldState.PestControl));
			States.Add(EMaizeFieldState.MaturePlant, new MF_MaturePlantState(maizeFieldStateContext, EMaizeFieldState.MaturePlant));
			States.Add(EMaizeFieldState.Harvesting, new MF_HarvestingState(maizeFieldStateContext, EMaizeFieldState.Harvesting));

			// Setting Current State or FirstState
			CurrentState = States[EMaizeFieldState.Seed];
		}

        #endregion

        #region Public Methods

        //Overriden Method
        public override void ValidateConstraints() // Validating Refs
        {
			
        }

		/// <summary>
		/// This Method Changes The Maize Field's State to its Corresponding NextState
		/// </summary>
		public void TransitionToNextState()
		{
            currentMaizeFieldState = CurrentState as MaizeFeildBaseState;
            if (currentMaizeFieldState != null)
            {
                SwitchState(currentMaizeFieldState.GetCorrespondingNextState());
            }
        }
        #endregion
    }
}
