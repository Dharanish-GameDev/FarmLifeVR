using FARMLIFEVR.CROPS.MAIZE;
using FARMLIFEVR.EVENTSYSTEM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using FARMLIFEVR.SIMPLEINTERACTABLES;

namespace FARMLIFEVR.LAND
{
    public class Land : MonoBehaviour
    {
        #region Private Variables

        // Editor Exposed
        [SerializeField][Required] private Material soilMat;
        [SerializeField][Required] private Material tilledMat;
        [SerializeField][Required] private Material wateredMat;
        [SerializeField][Required] private Renderer Renderer;
        [SerializeField][Required] private Maize maize;
        [SerializeField][Required] private SeedPlanterInteractable seedPlanterInteractable;

        [SerializeField] private LandState landState = LandState.BeforePloughing;

        // Editor Hidden
        private Material switchToMat;
        #endregion

        #region Properties

        public Maize Maize => maize;
        public LandState CurrentLandState
        {
            get
            {
                return landState;
            }
            set
            {
                landState = value;
                OnLandStateChanged(landState);
            }
        }

        #endregion

        #region LifeCycle Methods

        private void Awake()
        {
            ValidateConstraints();
            CurrentLandState = landState;
            maize.DisableAllVisualInHashSet();
            seedPlanterInteractable.OnTriedToPlant += SeedPlanterInteractable_OnTriedToPlant;
           
        }
        private void OnDisable()
        {
            seedPlanterInteractable.OnTriedToPlant -= SeedPlanterInteractable_OnTriedToPlant;
        }
        private void SeedPlanterInteractable_OnTriedToPlant()
        {
            Debug.Log($"{gameObject.name}'s SeedPlanterInteractable Tried To Plant");
            seedPlanterInteractable.gameObject.SetActive( false );
            maize.IsSeedPlanted = true;
        }

        private void Update()
        {
           if(Input.GetKeyDown(KeyCode.Alpha9))
           {
                AdvanceLandState();
           }
        }
        #endregion

        #region Private Methods

        private void OnLandStateChanged(LandState landState)
        {
            switchToMat = soilMat;
            switch (landState)
            {
                case LandState.BeforePloughing:
                    switchToMat = soilMat;
                    seedPlanterInteractable.gameObject.SetActive(false);
                    break;

                case LandState.Ploughed:
                    switchToMat = tilledMat;
                    break;

                case LandState.Watered:
                    switchToMat = wateredMat;
                    seedPlanterInteractable.gameObject.SetActive(false);
                    break;
            }
            GetComponent<Renderer>().material = switchToMat;
        }

        private void ValidateConstraints()
        {
            Assert.IsNotNull(soilMat, $"{this.gameObject.name}'s Soil Material is Null!");
            Assert.IsNotNull(tilledMat, $"{this.gameObject.name}'s Tilled Material is Null!");
            Assert.IsNotNull(wateredMat, $"{this.gameObject.name}'s Watered Material is Null!");
            Assert.IsNotNull(Renderer, $"{this.gameObject.name}'s Renderer is Null!");
            Assert.IsNotNull(maize, $"{this.gameObject.name}'s Maize is Null!");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the State of the Land to the Given LandState
        /// </summary>
        /// <param name="landState"></param>
        public void ChangeLandState(LandState landState)
        {
            CurrentLandState = landState; // Switching the LandState to the Given Value
            // This Change in State will Invoke the OnStateChanged Method
        }
        public void AdvanceLandState()
        {
            if (landState == LandState.BeforePloughing) ChangeLandState(LandState.Ploughed);
            else if (landState == LandState.Ploughed) ChangeLandState(LandState.Watered);
            else if (landState == LandState.Watered) ChangeLandState(LandState.BeforePloughing);
        }


        // It will Enable the Seed PlanterInteractable when the Land Entered Ploughed State.
        public void EnableSeedPlanterInteractable()
        {
            if (landState != LandState.Ploughed) return;
            if (maize.IsSeedPlanted) return; // Checks the Plants is Already Planted Before Enabling SeedPlanter Interactable 
            seedPlanterInteractable.gameObject.SetActive(true);
        }

        #endregion
    }

    public enum LandState
    {
        BeforePloughing,Ploughed,Watered
    }
}

