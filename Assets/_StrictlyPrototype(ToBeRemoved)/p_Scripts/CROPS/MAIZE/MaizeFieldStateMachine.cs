using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FARMLIFEVR.STATEMACHINE;
using FARMLIFEVR.EVENTSYSTEM;
using FARMLIFEVR.LAND;
using QFSW.QC;
using System.Linq;
using FARMLIFEVR.SIMPLEINTERACTABLES;
using FARMLIFEVR.FARMTOOLS;
using Mono.CSharp;
using UnityEngine.Assertions;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public class MaizeFieldStateMachine : StateManager<MaizeFieldStateMachine.EMaizeFieldState>
	{
		public enum EMaizeFieldState
		{
			Seed,
			Sprouting,
			WaterNeeded,
			Pest,
			Fertilizing,
			PestControl,
			ShoutBirds,
			Harvesting,
			AfterHarvesting
		}

		#region Private Variables

		[Header("References")]
		[Space(3)]

		[SerializeField,Required] private PipeInteractable pipeInteractable;
		[SerializeField,Required] private PesticideSprayerInteractable pesticideSprayerInteractable;
		[SerializeField,Required] private Tool_MegaphoneInteractable megaphoneInteractable;


		[Space(5)]
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
            EventManager.StartListening(EventNames.MF_AdvanceToNextState,TransitionToNextState);
        }
        public override void Awake()
		{
			base.Awake();
			maizeFieldStateContext = new MaizeFieldContext(
				this,
				maizesHashSet,
				pipeInteractable,
				pesticideSprayerInteractable,
				megaphoneInteractable
				);


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
            EventManager.StopListening(EventNames.MF_AdvanceToNextState,TransitionToNextState);
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
			States.Add(EMaizeFieldState.Pest, new MF_PestState(maizeFieldStateContext, EMaizeFieldState.Pest));
			States.Add(EMaizeFieldState.Fertilizing, new MF_FertilizingPlantState(maizeFieldStateContext, EMaizeFieldState.Fertilizing));
			States.Add(EMaizeFieldState.PestControl, new MF_PestControlState(maizeFieldStateContext, EMaizeFieldState.PestControl));
			States.Add(EMaizeFieldState.ShoutBirds, new MF_ShoutBirdsPlantState(maizeFieldStateContext, EMaizeFieldState.ShoutBirds));
			States.Add(EMaizeFieldState.Harvesting, new MF_HarvestingState(maizeFieldStateContext, EMaizeFieldState.Harvesting));
			States.Add(EMaizeFieldState.AfterHarvesting, new MF_AfterHarvestingState(maizeFieldStateContext, EMaizeFieldState.AfterHarvesting));

			// Setting Current State or FirstState
			CurrentState = States[EMaizeFieldState.Seed];
		}


		//It  will Enable the SeedPlanter Interactable for all The Land in The hashSet.
		// It Will lead to the Next Task Planting in the Next day.
		private void EnableRootInteractableInLandsHashSet()
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
		public void PlanterInteractable()
		{
			EnableRootInteractableInLandsHashSet();
        }

		// Need to be Removed Method
		[Command]
		public void PlantsSeeds()
		{
            foreach (Land land in landsHashSet)
            {
				land.PlantSeed();
            }
        }

		[Command]
		public void Pest()
		{
			foreach(Land land in landsHashSet)
			{
				land.Maize.IsPestSprayed = true;
			}
		}

		[Command]
		public void Fert()
		{
            foreach (Land land in landsHashSet)
            {
                land.Maize.IsFertilized = true;
            }
        }

		[Command]
		public void Shout()
		{
			megaphoneInteractable.SetMaxShoutCount();
		}


        //Overriden Method
        public override void ValidateConstraints() // Validating Refs
        {
			if (landsList.Count == 0) Debug.LogError("Maizes List is Empty !");
			Assert.IsNotNull(pipeInteractable, "Pipe Interactable is Null !");
			Assert.IsNotNull(pesticideSprayerInteractable, "Pesticide Sprayer Interactable is Null!");
			Assert.IsNotNull(megaphoneInteractable, "Megaphone Interactable is Null!");
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

		public bool IsInWaterNeededState()
		{
			return CurrentState.GetStateKey() == EMaizeFieldState.WaterNeeded;
		}

		public void MakeAllSeedsUnplanted()
		{
			landsHashSet.ToList().ForEach(land => land.Maize.IsSeedPlanted = false);
		}

		public void MakeAllPlantsUnFertilized()
		{
			landsHashSet.ToList().ForEach(land=>land.Maize.IsFertilized = false);
		}

        #region Conditions to Switch State
        public bool IsAllSeedsPlanted()
		{
			return landsHashSet.All(land => land.Maize.IsSeedPlanted);
		}
		public bool IsAllWaterCanalsGrubbed()
		{
			return GameManager.Instance.IrrigationManager.IsAllCanalsGrubbed();
		}
		public bool IsAllPlantsWatered()
		{
			return landsHashSet.All(x=>x.Maize.IsWatered);
		}
		public bool IsAllPlantsFertilized()
		{
            return landsHashSet.All(x => x.Maize.IsFertilized);
        }
		public bool IsPestSprayedToAllPlants()
		{
			return landsHashSet.All(a => a.Maize.IsPestSprayed);
		}
		public bool IsMaxBirdShoutCountReached()
		{
			return megaphoneInteractable.MaxShoutCountReached;
		}

        #endregion

        #endregion
    }
}
