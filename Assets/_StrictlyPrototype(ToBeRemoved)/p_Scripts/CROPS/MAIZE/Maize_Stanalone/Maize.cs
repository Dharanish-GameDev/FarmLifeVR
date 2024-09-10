using FARMLIFEVR.EVENTSYSTEM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.CROPS.MAIZE
{
    public class Maize : MonoBehaviour
    {
        #region Private Variables

        //Hidden
        private MaizeFeildStateMachine.EMaizeFieldState currentMaizeFieldState = MaizeFeildStateMachine.EMaizeFieldState.Seed;
        private readonly HashSet<GameObject> visualsHashSet = new HashSet<GameObject>();


        [Header("References")]
        [Space(3)]
        [SerializeField] private MaizeVisuals maizeVisuals = new MaizeVisuals();


        private bool isSeedPlanted = false;

        #endregion

        #region Properties

        public bool IsSeedPlanted 
        {
            get
            {
                return isSeedPlanted;
            }
            set 
            { 
                isSeedPlanted = value;
                OnSeedPlanted(value);
            }
        }

        private void OnSeedPlanted(bool value)
        {
            if (value)
            {
                EnableVisualFromHashSet(maizeVisuals.SeedVisual);
            }
        }

        #endregion

        #region LifeCycle Methods

        private void OnEnable()
        {
            EventManager.StartListening(EventNames.MF_OnStateChanged, OnMaizeFieldStateChanged);
        }
        private void Awake()
        {
            AddVisualsToHashset();
        }
        private void OnDisable()
        {
            EventManager.StopListening(EventNames.MF_OnStateChanged, OnMaizeFieldStateChanged);
        }

        #endregion

        #region Private Methods

        private void OnMaizeFieldStateChanged(params object[] parameters)
        {
            if (parameters.Length == 0) return;
            currentMaizeFieldState = (MaizeFeildStateMachine.EMaizeFieldState)parameters[0]; // Getting Current State from the Params
            print($"{gameObject.name} : Entered {currentMaizeFieldState} State!");
            switch (currentMaizeFieldState)
            {
                // Handles Seed State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.Seed:
                    SeedState();
                    break;


                // Handles Sprouting State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.Sprouting:
                    SproutingState();
                    break;

                    
                // Handles WaterNeeded State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.WaterNeeded:
                    WaterNeedState();
                    break;


                // Handles SmallPlant State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.SmallPlant:
                    SmallPlantState();
                    break;


                // Handles MediumPlant State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.MediumPlant:
                    MediumPlantState();
                    break;


                // Handles PestControl State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.PestControl:
                    PestControlState();
                    break;


                // Handles MaturePlant State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.MaturePlant:
                    MaturePlantState();
                    break;


                // Handles Harvesting State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.Harvesting:
                    HarvestingState();
                    break;


                // Handles After Harvesting State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.AfterHarvesting:
                    AfterHarvestingState();
                    break;

            }
        }

        #region States Entered Callback Methods

        private void SeedState()
        {
            DisableAllVisualInHashSet();
        }
        private void SproutingState()
        {
            EnableVisualFromHashSet(maizeVisuals.SproutingVisual);
        }
        private void WaterNeedState()
        {
            EnableVisualFromHashSet(maizeVisuals.WaterNeededVisual);
        }
        private void SmallPlantState()
        {
            EnableVisualFromHashSet(maizeVisuals.SmallPlantVisual);
        }
        private void MediumPlantState()
        {
            EnableVisualFromHashSet(maizeVisuals.MediumPlantVisual);
        }
        private void PestControlState()
        {
            EnableVisualFromHashSet(maizeVisuals.PestControlVisual);
        }
        private void MaturePlantState()
        {
            EnableVisualFromHashSet(maizeVisuals.MaturePlant);
        }
        private void HarvestingState()
        {
            EnableVisualFromHashSet(maizeVisuals.HarvestReady);
            maizeVisuals.HarvestReadyMaizeModel.SetActive(true);
        }
        private void AfterHarvestingState()
        {
            EnableVisualFromHashSet(maizeVisuals.AfterHarvest);
        }

        #endregion

        // HashSet Methods

        //This Method will Add all the Maize Visual into the Hashset For Easier Loop Throughs
        private void AddVisualsToHashset()
        {
            visualsHashSet.Add(maizeVisuals.SeedVisual);
            visualsHashSet.Add(maizeVisuals.SproutingVisual);
            visualsHashSet.Add(maizeVisuals.WaterNeededVisual);
            visualsHashSet.Add(maizeVisuals.SmallPlantVisual);
            visualsHashSet.Add(maizeVisuals.MediumPlantVisual);
            visualsHashSet.Add(maizeVisuals.PestControlVisual);
            visualsHashSet.Add(maizeVisuals.MaturePlant);
            visualsHashSet.Add(maizeVisuals.HarvestReady);
            visualsHashSet.Add(maizeVisuals.HarvestReadyMaizeModel);
            visualsHashSet.Add(maizeVisuals.AfterHarvest);
        }

        

        private void EnableVisualFromHashSet(GameObject visualToEnable)
        {
            DisableAllVisualInHashSet();
            visualToEnable.SetActive(true);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// It Would disable all the Visual Elements of The Maize
        /// </summary>
        public void DisableAllVisualInHashSet()
        {
            foreach (var visual in visualsHashSet)
            {
                visual.SetActive(false);
            }
        }
        #endregion
    }

    [System.Serializable]
    public struct MaizeVisuals
    {
        [SerializeField] [Required] private GameObject seedVisual;
        [SerializeField] [Required] private GameObject sproutingVisual;
        [SerializeField] [Required] private GameObject waterNeededVisual;
        [SerializeField] [Required] private GameObject smallPlantVisual;
        [SerializeField] [Required] private GameObject mediumPlantVisual;
        [SerializeField] [Required] private GameObject pestControlVisual;
        [SerializeField] [Required] private GameObject maturePlantVisual;
        [SerializeField] [Required] private GameObject harvestReadyVisual;
        [SerializeField] [Required] private GameObject harvestReadyMaizeModelVisual;
        [SerializeField] [Required] private GameObject afterHarvest;

        // Properties

        public GameObject SeedVisual => seedVisual;
        public GameObject SproutingVisual => sproutingVisual;
        public GameObject WaterNeededVisual => waterNeededVisual;
        public GameObject SmallPlantVisual => smallPlantVisual;
        public GameObject MediumPlantVisual => mediumPlantVisual;
        public GameObject PestControlVisual => pestControlVisual;
        public GameObject MaturePlant => maturePlantVisual;
        public GameObject HarvestReady => harvestReadyVisual;
        public GameObject HarvestReadyMaizeModel => harvestReadyMaizeModelVisual;
        public GameObject AfterHarvest => afterHarvest;
    }
}