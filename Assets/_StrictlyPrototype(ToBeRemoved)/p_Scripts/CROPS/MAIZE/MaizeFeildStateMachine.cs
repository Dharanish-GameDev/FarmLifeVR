using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FARMLIFEVR.STATEMACHINE;
using UnityEngine.Assertions;
using FARMLIFEVR.EVENTSYSTEM;
using FARMLIFEVR.LAND;
using QFSW.QC;
using System.Runtime.InteropServices;
using System.Linq;

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
			Harvesting,
			AfterHarvesting
		}

		#region Private Variables

		//Exposed in Editor
		[SerializeField] private List<Land> landsList = new List<Land>();

    
        //Hidden from Editor
        private MaizeFieldContext maizeFieldStateContext;
		private MaizeFeildBaseState currentMaizeFieldState;
        private readonly HashSet<Maize> maizesHashSet = new HashSet<Maize>(); // Using HashSet to avoid Duplicity
		private readonly HashSet<Land> landsHashSet = new HashSet<Land>();

        #endregion

        #region Properties

		public HashSet<Land> LandsHashSet => landsHashSet;

        #endregion

        #region LifeCycle Methods

        private void OnEnable()
        {
			EventManager.StartListening(EventNames.MF_AdvanceToNextState, TransitionToNextState);
        }
        public override void Awake()
		{
			base.Awake();
			maizeFieldStateContext = new MaizeFieldContext(this,maizesHashSet);
			InitializeStates();
			foreach (Land land in landsList)  // Adding All the Elements in the List to The HashSet for Usage
			{
				maizesHashSet.Add(land.Maize);
				landsHashSet.Add(land);
			}
		    landsList.Clear();
		}
        private void OnDisable()
        {
            EventManager.StopListening(EventNames.MF_AdvanceToNextState, TransitionToNextState);
        }

        public override void Update()
		{
			base.Update();
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
				EventManager.TriggerEvent(EventNames.MF_AdvanceToNextState);
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
			States.Add(EMaizeFieldState.AfterHarvesting, new MF_AfterHarvestingState(maizeFieldStateContext, EMaizeFieldState.AfterHarvesting));

			// Setting Current State or FirstState
			CurrentState = States[EMaizeFieldState.Seed];
		}


		//It  will Enable the SeedPlanter Interactable for all The Land in The hashSet.
		// It Will lead to the Next Task Planting in the Next day.
		private void EnableSeedPlanterInteractableInLandsHashSet()
		{
            foreach (Land land in landsHashSet)
            {
				land.EnableSeedPlanterInteractable();
            }
        }

		#endregion

		#region Public Methods

		// Need to be Removed Method
		[Command]
		public void PlanterInterctable()
		{
			EnableSeedPlanterInteractableInLandsHashSet();
        }

        //Overriden Method
        public override void ValidateConstraints() // Validating Refs
        {
			if (landsList.Count == 0) Debug.LogError("Maizes List is Empty !");
        }

		/// <summary>
		/// This Method Changes The Maize Field's State to its Corresponding NextState
		/// </summary>
		public void TransitionToNextState()
		{
            currentMaizeFieldState = CurrentState as MaizeFeildBaseState;
            if (currentMaizeFieldState != null)
            {
				if (!currentMaizeFieldState.GetHasApprovalToSwitchState()) return;
                SwitchState(currentMaizeFieldState.GetCorrespondingNextState());
            }
        }

		public bool IsAllSeedsPlanted()
		{
			return landsHashSet.All(land => land.Maize.IsSeedPlanted);
		}
		
        #endregion
    }
}
