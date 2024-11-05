using FARMLIFEVR.EVENTSYSTEM;
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
        private bool isWatered = false;
        private bool isPestSprayed =  false;
        private bool isFertilized = false;
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

        public bool IsWatered
        {
            get
            {
                return isWatered;
            }
            set 
            {
                isWatered = value;
                OnWatered(value);
            }
        }

        public bool IsPestSprayed
        {
            get
            {
                return isPestSprayed;
            }
            set
            {
                isPestSprayed = value;
                OnPestSprayed(value);
            }
        }

        public bool IsFertilized
        {
            get
            {
                return isFertilized;
            }
            set
            {
                isFertilized = value;
                OnFertilized(value);
            }
        }

        public bool isInSeedState => currentMaizeFieldState == MaizeFeildStateMachine.EMaizeFieldState.Seed;

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
            //print($"{gameObject.name} : Entered {currentMaizeFieldState} State!");
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
                case MaizeFeildStateMachine.EMaizeFieldState.Pest:
                    PestState();
                    break;


                // Handles MediumPlant State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.Fertilizing:
                    FertilizingPlantState();
                    break;


                // Handles PestControl State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.PestControl:
                    PestControlState();
                    break;


                // Handles MaturePlant State Logic
                case MaizeFeildStateMachine.EMaizeFieldState.ShoutBirds:
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
            isWatered = false;
            maizeVisuals.WaterNeededUI.SetActive(true);
        }
        private void PestState()
        {
            EnableVisualFromHashSet(maizeVisuals.PestPlantVisual);
            isPestSprayed = false;
            maizeVisuals.ActualInfectedPart.SetActive(true);

            if (maizeVisuals.InfectedPartsTransform.Length > 0)
            {
                int randomIndex = Random.Range(0, maizeVisuals.InfectedPartsTransform.Length - 1);
                maizeVisuals.ActualInfectedPart.transform.position = maizeVisuals.InfectedPartsTransform[randomIndex].position;
            }


        }
        private void FertilizingPlantState()
        {
            EnableVisualFromHashSet(maizeVisuals.FertilizingPlantVisual);
        }
        private void PestControlState()
        {
            EnableVisualFromHashSet(maizeVisuals.PestControlVisual);
        }
        private void MaturePlantState()
        {
            EnableVisualFromHashSet(maizeVisuals.ShoutBirdsPlantState);
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
            visualsHashSet.Add(maizeVisuals.WaterNeededUI);
            visualsHashSet.Add(maizeVisuals.PestPlantVisual);
            visualsHashSet.Add(maizeVisuals.ActualInfectedPart);
            visualsHashSet.Add(maizeVisuals.FertilizingPlantVisual);
            visualsHashSet.Add(maizeVisuals.FertilizedAreaVisual);
            visualsHashSet.Add(maizeVisuals.PestControlVisual);
            visualsHashSet.Add(maizeVisuals.ShoutBirdsPlantState);
            visualsHashSet.Add(maizeVisuals.HarvestReady);
            visualsHashSet.Add(maizeVisuals.HarvestReadyMaizeModel);
            visualsHashSet.Add(maizeVisuals.AfterHarvest);
        }
        private void EnableVisualFromHashSet(GameObject visualToEnable)
        {
            DisableAllVisualInHashSet();
            visualToEnable.SetActive(true);
        }


        #region OnPropertyChanged Methods

        private void OnFertilized(bool value)
        {
            if (value)
            {
                maizeVisuals.FertilizedAreaVisual.SetActive(true); // Enabling the Fertilized area to show the plant is actually fertilized
            }
        }

        private void OnPestSprayed(bool value)
        {
            if (value)
            {
                maizeVisuals.ActualInfectedPart.SetActive(false);
            }
        }

        private void OnWatered(bool value)
        {
            if (value)
            {
                maizeVisuals.WaterNeededUI.SetActive(false);
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

        [Space(5)]
        [Header("Water State Refs")]
        [Space(3)]
        [SerializeField] [Required] private GameObject waterNeededVisual;
        [SerializeField] [Required] private GameObject waterNeededUI;

        [Space(5)]
        [Header("Pest State Refs")]
        [Space(3)]
        [SerializeField] [Required] private GameObject pestPlantVisual;
        [SerializeField] [Required] private GameObject actualInfectedPart;
        [SerializeField] private Transform[] infectedPartsLocation;

        [Space(5)]
        [Header("Fertilizing State Refs")]
        [Space(3)]
        [SerializeField] [Required] private GameObject feritilizingPlantVisual;
        [SerializeField] [Required] private GameObject feritilizedArea;

        [Space(5)]

        [SerializeField] [Required] private GameObject pestControlVisual;
        [SerializeField] [Required] private GameObject shoutBirdsPlantVisual;
        [SerializeField] [Required] private GameObject harvestReadyVisual;
        [SerializeField] [Required] private GameObject harvestReadyMaizeModelVisual;
        [SerializeField] [Required] private GameObject afterHarvest;

        // Properties

        public GameObject SeedVisual => seedVisual;
        public GameObject SproutingVisual => sproutingVisual;
        public GameObject WaterNeededVisual => waterNeededVisual;
        public GameObject WaterNeededUI => waterNeededUI;
        public GameObject PestPlantVisual => pestPlantVisual;
        public GameObject ActualInfectedPart => actualInfectedPart;
        public Transform[] InfectedPartsTransform => infectedPartsLocation;
        public GameObject FertilizingPlantVisual => feritilizingPlantVisual;
        public GameObject FertilizedAreaVisual => feritilizedArea;
        public GameObject PestControlVisual => pestControlVisual;
        public GameObject ShoutBirdsPlantState => shoutBirdsPlantVisual;
        public GameObject HarvestReady => harvestReadyVisual;
        public GameObject HarvestReadyMaizeModel => harvestReadyMaizeModelVisual;
        public GameObject AfterHarvest => afterHarvest;
    }
}