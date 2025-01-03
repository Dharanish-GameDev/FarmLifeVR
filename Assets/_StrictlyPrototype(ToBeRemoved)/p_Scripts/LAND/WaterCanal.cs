using Mono.CSharp;
using Mono.CSharp.yyParser;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace FARMLIFEVR.LAND
{
    public class WaterCanal : MonoBehaviour
    {
        [Header("Materials")]
        [Space(3)]
        [SerializeField][Required] private Material dirtMat;
        [SerializeField][Required] private Material grubbedMat;
        [SerializeField][Required] private Material WateredMat;

        [Space(5)]

        [Header("References")]
        [Space(3)]
        [SerializeField][Required] private Renderer meshRenderer;
        [SerializeField][Required] private GameObject waterVisual;
        [SerializeField] private List<Land> landsColumnList = new List<Land>();

        [Space(5)]

        [Header("LayerMasks")]
        [Space(3)]
        [SerializeField] private LayerMask waterCanalMainLayerMask;
        [SerializeField] private LayerMask invalidLayerMask;

        private bool isGrubbed = false;

        private int currentHit = 0;
        private readonly int maxHit = 2;

        #region LifeCycle Methods

        private void Awake()
        {
            waterCanalMainLayerMask = gameObject.layer;
            waterVisual.SetActive(false);
        }

        #endregion

        #region Properties

        public bool IsGrubbed => isGrubbed;

        #endregion

        #region Public Methods

        /// <summary>
        /// Its a Callback method to the Hoe Interactable's Overlap Checker to grub the Land
        /// </summary>
        public void GrubLand()
        {
            if (isGrubbed) return;

            if (currentHit < maxHit)
            {
                currentHit++;
            }

            if (currentHit != maxHit) return;
            isGrubbed = true;
            ChangeMaterial(grubbedMat);
            gameObject.layer = invalidLayerMask; // Its To Avoid Further OverLapping Registed
            currentHit = 0;
            if(isGrubbed)
                GameManager.Instance?.MaizeFieldStateMachine?.CheckAllWaterCanalsGrubbed();
        }

        /// <summary>
        /// Its To Enable the water Visual Block
        /// </summary>
        public void EnableWaterVisual()
        {
            waterVisual.SetActive(true);
        }

        /// <summary>
        /// This Method Triggers a coroutine which irrigate the Land Column List One by One
        /// </summary>
        public void IrrigateColumnLands()
        {
            StartCoroutine(IrrigateLands());
        }

        /// <summary>
        /// This method resets water canal to its original initial state 
        /// </summary>
        public void ResetWaterCanal()
        {
            UnGrubLand();
            UnIrrigateLand();
            waterVisual.SetActive(false);
        }

        public void AlignLandsWaterBlock(Vector3 localPos)
        {
            foreach(var land in landsColumnList)
            {
                land.SetIndividualWaterBlockLocalPosition(localPos);
            }
        }

        public void DisableIndividualWaterBlocksVisual()
        {
            foreach (var land in landsColumnList)
            {
                land.DisableIndividualWaterBlock();
            }
        }

        #endregion

        #region Private Methods

        private void ChangeMaterial(Material mat)
        {
            meshRenderer.material = mat;
        }

        /// <summary>
        /// This Method actually irrigates the Land Present Within its Column
        /// </summary>
        /// <returns></returns>
        private IEnumerator IrrigateLands()
        {
            if(landsColumnList.All(x => x.Maize.IsWatered)) yield return null;
            
            foreach(var land in landsColumnList)
            {
                yield return new WaitForSeconds(1);
                land.Maize.IsWatered = true;
                land.EnableIndividualWaterBlock();
            }
        }

        // Ungrubbing the Land of the Water Canal for the Next day.
        private void UnGrubLand()
        {
            if (!isGrubbed) return;
            isGrubbed = false;
            ChangeMaterial(dirtMat);
            gameObject.layer = waterCanalMainLayerMask; // Its To Avoid Further OverLapping Registed
        }

        //It UnIrrigates the Land By Setting the IsWatered Property of The Maize to False and Disabling its Individual Water Blocks.
        private void UnIrrigateLand()
        {
            if (landsColumnList.All(x => !x.Maize.IsWatered)) return;

            foreach (var land in landsColumnList)
            {
                land.Maize.IsWatered = false;
                land.DisableIndividualWaterBlock();
            }
        }


        #endregion
    }
}
