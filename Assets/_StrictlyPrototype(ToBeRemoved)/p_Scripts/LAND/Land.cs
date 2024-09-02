using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.LAND
{
    public class Land : MonoBehaviour
    {
        #region Private Variables

        // Editor Exposed
        [SerializeField] private Material soilMat;
        [SerializeField] private Material tilledMat;
        [SerializeField] private Material wateredMat;
        [SerializeField] private LandState LandState = LandState.BeforePloughing;
        [SerializeField] private Renderer Renderer;

        // Editor Hidden
        private Material switchToMat;
        #endregion

        #region Properties



        #endregion

        #region LifeCycle Methods

        private void Awake()
        {

        }
        private void Start()
        {
            
        }
        private void Update()
        {

        }

        #endregion

        #region Private Methods


        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the State of the Land to the Given LandState
        /// </summary>
        /// <param name="landState"></param>
        public void ChangeLandState(LandState landState)
        {
            LandState = landState; // Switching the LandState to the Given Value
            switchToMat = soilMat;
            switch (landState)
            {
                case LandState.BeforePloughing:
                    switchToMat = soilMat;
                    break;

                case LandState.Ploughed:
                    switchToMat = tilledMat;
                    break;

                case LandState.Watered:
                    switchToMat = wateredMat;
                    break;
            }
            GetComponent<Renderer>().material = switchToMat;
        }

        #endregion
    }

    public enum LandState
    {
        BeforePloughing,Ploughed,Watered
    }
}

