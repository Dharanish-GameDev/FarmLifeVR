using FARMLIFEVR.EVENTSYSTEM;
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
        #endregion

        #region Properties

        

        #endregion

        #region LifeCycle Methods

        private void OnEnable()
        {
            EventManager.StartListening(EventNames.MF_OnStateChanged, OnMaizeFieldStateChanged);
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
            currentMaizeFieldState = (MaizeFeildStateMachine.EMaizeFieldState)parameters[0];
            switch (currentMaizeFieldState)
            {
                case MaizeFeildStateMachine.EMaizeFieldState.Seed:
                    SeedState();
                    break;

                case MaizeFeildStateMachine.EMaizeFieldState.Sprouting:
                    SproutingState();
                    break;

                case MaizeFeildStateMachine.EMaizeFieldState.WaterNeeded:
                    WaterNeedState();
                    break;

                case MaizeFeildStateMachine.EMaizeFieldState.SmallPlant:
                    SmallPlantState();
                    break;

                case MaizeFeildStateMachine.EMaizeFieldState.MediumPlant:
                    MediumPlantState();
                    break;

                case MaizeFeildStateMachine.EMaizeFieldState.PestControl:
                    PestControlState();
                    break;

                case MaizeFeildStateMachine.EMaizeFieldState.MaturePlant:
                    MaturePlantState();
                    break;

                case MaizeFeildStateMachine.EMaizeFieldState.Harvesting:
                    HarvestingState();
                    break;

            }
        }

        #region States Entered Callback Methods
        private void SeedState()
        {
            Debug.Log("Seed State");
            EnableVisualFromHashSet(maizeVisuals.SeedVisual);
        }
        private void SproutingState()
        {
            Debug.Log("Sprouting State");
            EnableVisualFromHashSet(maizeVisuals.SproutingVisual);
        }
        private void WaterNeedState()
        {
            Debug.Log("WaterNeeded State");
            EnableVisualFromHashSet(maizeVisuals.WaterNeededVisual);
        }
        private void SmallPlantState()
        {
            Debug.Log("SmallPlant State");
            EnableVisualFromHashSet(maizeVisuals.SmallPlantVisual);
        }
        private void MediumPlantState()
        {
            Debug.Log("MediumPlant State");
            EnableVisualFromHashSet(maizeVisuals.MediumPlantVisual);
        }
        private void PestControlState()
        {
            Debug.Log("Pest Control State");
            EnableVisualFromHashSet(maizeVisuals.PestControlVisual);
        }
        private void MaturePlantState()
        {
            Debug.Log("MaturePlant State");
            EnableVisualFromHashSet(maizeVisuals.MaturePlant);
        }
        private void HarvestingState()
        {
            Debug.Log("Harvestting State");
            EnableVisualFromHashSet(maizeVisuals.HarvestReady);
            maizeVisuals.HarvestReadyMaizeModel.SetActive(true);
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

        private void DisableAllVisualInHashSet()
        {
            foreach (var visual in visualsHashSet)
            {
                visual.SetActive(false);
            }
        }

        private void EnableVisualFromHashSet(GameObject visualToEnable)
        {
            DisableAllVisualInHashSet();
            visualToEnable.SetActive(true);
        }

        #endregion

        #region Public Methods


        #endregion
    }

    [System.Serializable]
    public struct MaizeVisuals
    {
        [SerializeField] [Required] private GameObject seed;
        [SerializeField] [Required] private GameObject sprouting;
        [SerializeField] [Required] private GameObject waterNeeded;
        [SerializeField] [Required] private GameObject smallPlant;
        [SerializeField] [Required] private GameObject mediumPlant;
        [SerializeField] [Required] private GameObject pestControl;
        [SerializeField] [Required] private GameObject maturePlant;
        [SerializeField] [Required] private GameObject harvestReady;
        [SerializeField] [Required] private GameObject harvestReadyMaizeModel;
        [SerializeField] [Required] private GameObject afterHarvest;

        // Properties

        public GameObject SeedVisual => seed;
        public GameObject SproutingVisual => sprouting;
        public GameObject WaterNeededVisual => waterNeeded;
        public GameObject SmallPlantVisual => smallPlant;
        public GameObject MediumPlantVisual => mediumPlant;
        public GameObject PestControlVisual => pestControl;
        public GameObject MaturePlant => maturePlant;
        public GameObject HarvestReady => harvestReady;
        public GameObject HarvestReadyMaizeModel => harvestReadyMaizeModel;
        public GameObject AfterHarvest => afterHarvest;
    }
}