using FARMLIFEVR.CROPS.MAIZE;
using FARMLIFEVR.EVENTSYSTEM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using FARMLIFEVR.SIMPLEINTERACTABLES;
using UnityEngine.Serialization;


namespace FARMLIFEVR.LAND
{
    public class Land : MonoBehaviour
    {
        #region Private Variables

        // Editor Exposed
        [SerializeField][Required] private Maize maize;
        [SerializeField][Required] private MaizeRootInteractable maizeRootInteractable;

        [SerializeField] private LandState landState = LandState.BeforePloughing;


        [Space(5)]
        [Header("Land Visuals Struct")]
        [SerializeField] private LandVisuals landVisuals;

        // Editor Hidden
        private Material switchToMat;
        #endregion

        #region Properties

        public Maize Maize => maize;
        
        public bool IsPloughed => landState == LandState.Ploughed;
        
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
            maizeRootInteractable.OnTriedToInteractRoot += MaizeRootInteractable_OnTriedToInteract;
           
        }
        private void OnDisable()
        {
            maizeRootInteractable.OnTriedToInteractRoot -= MaizeRootInteractable_OnTriedToInteract;
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
            switch (landState)
            {
                case LandState.BeforePloughing:
                    landVisuals.LandMeshRenderer.enabled = false;
                    maizeRootInteractable.gameObject.SetActive(false);
                    break;

                case LandState.Ploughed:
                    landVisuals.LandMeshRenderer.enabled = true;
                    GameManager.Instance?.MaizeFieldStateMachine?.CheckPlougingMissionConclusion();
                    landVisuals.LandMeshRenderer.material = landVisuals.LandBlockMat;
                    break;

                case LandState.Watered:
                    landVisuals.LandMeshRenderer.material = landVisuals.WateredMat;
                    maizeRootInteractable.gameObject.SetActive(false);
                    break;
            }
        }
        private void ValidateConstraints()
        {
            Assert.IsNotNull(landVisuals.LandMeshRenderer,$"{this.gameObject.name}'s LandMeshRenderer is Null!");
            Assert.IsNotNull(landVisuals.LandBlockMat, $"{this.gameObject.name}'s Tilled Material is Null!");
            Assert.IsNotNull(landVisuals.WateredMat, $"{this.gameObject.name}'s Watered Material is Null!");
            Assert.IsNotNull(maize, $"{this.gameObject.name}'s Maize is Null!");
        }
        private void MaizeRootInteractable_OnTriedToInteract()
        {
            maizeRootInteractable.gameObject.SetActive(false);
            if(maize.isInSeedState)
            {
                maize.IsSeedPlanted = true;
            }
            else
            {
                maize.IsFertilized = true;
            }
            
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
            if (maize.IsSeedPlanted && maize.IsFertilized) return; // Checks the Plants is Already Planted or Fertilized Before Enabling
            maizeRootInteractable.gameObject.SetActive(true);
        }

        /// <summary>
        /// This Method sets the Individual Water Blocks Local Pos which belongs to Land and the Water Block will Activate when the Land is Being Irrigated
        /// </summary>
        public void SetIndividualWaterBlockLocalPosition(Vector3 localPos)
        {
            localPos.y = landVisuals.IndividualWaterBlock.localPosition.y;
            landVisuals.IndividualWaterBlock.localPosition = localPos;
        }

        /// <summary>
        /// This Method will Enable the Land's Water Block Visual during Irrigarion
        /// </summary>
        public void EnableIndividualWaterBlock()
        {
            landVisuals.IndividualWaterBlock.gameObject.SetActive(true);
        }

        /// <summary>
        /// This Method will Disable the Land's Water Block Visual during UnIrrigarion
        /// </summary>
        public void DisableIndividualWaterBlock()
        {
            landVisuals.IndividualWaterBlock.gameObject.SetActive(false);
        }

        // NEED TO BE REMOVED
        public void PlantSeed()
        {
            MaizeRootInteractable_OnTriedToInteract();
        }

        #endregion
    }

    public enum LandState
    {
        BeforePloughing,Ploughed,Watered
    }

    /// <summary>
    /// A Struct that holds all the Visual Elements of the Land
    /// </summary>
    [System.Serializable]
    public struct LandVisuals
    {
        [SerializeField][Required] private MeshRenderer landMeshRenderer;
        [SerializeField][Required] private Material landBlockMat;
        [SerializeField][Required] private Material wateredMat;
        [SerializeField][Required] private Transform individualWaterBlock;


        #region Properties
        public MeshRenderer LandMeshRenderer => landMeshRenderer;
        public Material LandBlockMat => landBlockMat; 
        public Material WateredMat => wateredMat;
        public Transform IndividualWaterBlock => individualWaterBlock;

        #endregion
    }
}

