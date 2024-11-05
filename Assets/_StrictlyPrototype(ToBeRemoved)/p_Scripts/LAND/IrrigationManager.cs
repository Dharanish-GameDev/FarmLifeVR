using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
using System.Linq;
using FARMLIFEVR.EVENTSYSTEM;

namespace FARMLIFEVR.LAND
{
	public class IrrigationManager : MonoBehaviour
	{
		#region Private Variables

		[SerializeField] private List<WaterCanal> waterCanals = new List<WaterCanal>();
		[SerializeField] private float landWaterOddXOffset;
		[SerializeField] private float landWaterEvenXOffset;


		private Coroutine irrigationCouroutine = null;

		private List<WaterCanal> evenIndexCanal = new List<WaterCanal>();
		private List<WaterCanal> oddIndexCanal = new List<WaterCanal>();



		private Vector3 evenLandWaterLocalPos = Vector3.zero;
		private Vector3 oddLandWaterLocalPos = Vector3.zero;

        #endregion

        #region Properties

        #endregion

        #region LifeCycle Methods

        private void OnEnable()
        {
			EventManager.StartListening(EventNames.StartIrrigation, Irrigation);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventNames.StartIrrigation, Irrigation);
        }
        private void Awake()
        {
			evenLandWaterLocalPos.x = landWaterEvenXOffset;
			oddLandWaterLocalPos.x = landWaterOddXOffset;
			DisableWaterColumnsLandsIndividualWaterBlock();
        }
        #endregion

        #region Private Methods

        private IEnumerator IrrigateWaterCanals()
		{
			bool isEven = true;

			for (int i = 0; i < waterCanals.Count; i++)
			{
				isEven = !isEven;
				waterCanals[i].EnableWaterVisual();
				if (isEven)
				{
					evenIndexCanal.Add(waterCanals[i]);
					waterCanals[i].AlignLandsWaterBlock(evenLandWaterLocalPos);
					OnEvenIndexListChanged(evenIndexCanal.IndexOf(waterCanals[i]));
				}
				else
				{
					oddIndexCanal.Add(waterCanals[i]);
                    waterCanals[i].AlignLandsWaterBlock(oddLandWaterLocalPos);
                }
                yield return new WaitForSeconds(2); // The Wait Seconds Change per No of Elements in Column
			}
		}


		/// <summary>
		/// It Disable those Individual Water Blocks belongs to the Land, You Might Think we can reset the Whole Irrigation but we can't reset when its already UnIrrigated
		/// </summary>
		private void DisableWaterColumnsLandsIndividualWaterBlock()
		{
			foreach (var waterCanal in waterCanals)
			{
				waterCanal.DisableIndividualWaterBlocksVisual();
			}
		}

		#endregion

		#region Public Methods


		/// <summary>
		/// This Method should be called on the Tap Interactable to Start the Irrigation Process
		/// </summary>
		[Command]
		public void Irrigation()
		{
			if (!IsAllCanalsGrubbed()) return;
			if (!GameManager.Instance.MaizeFeildStateMachine.IsInWaterNeededState()) return;
			irrigationCouroutine = StartCoroutine(IrrigateWaterCanals());
		}

		// To Be Removed
		[Command]
		public void Grub()
		{
			if (IsAllCanalsGrubbed()) return;
			foreach (var waterCanal in waterCanals)
			{
				waterCanal.GrubLand();
				waterCanal.GrubLand();
			}
		}


		/// <summary>
		/// It Resets the Water Canals while exiting the Water Needed State
		/// </summary>
		[Command]
		public void ResetWaterCanal()
		{
			foreach (var waterCanal in waterCanals)
			{
				waterCanal.ResetWaterCanal();
            }
		}

		/// <summary>
		/// It Checks if all the Canals in the List are Grubbed
		/// </summary>
		/// <returns></returns>
        public bool IsAllCanalsGrubbed()
        {
            return waterCanals.All(x => x.IsGrubbed);
        }


		/// <summary>
		/// It Triggers when Even Index List Changed to Trigger the Irrigating the Individual Water Canal
		/// </summary>
		/// <param name="index"></param>
		private void OnEvenIndexListChanged(int index)
		{ 
			WaterCanal evenCanal = evenIndexCanal[index];
			WaterCanal oddcanal = oddIndexCanal[index];

			evenCanal.IrrigateColumnLands();
			oddcanal.IrrigateColumnLands();
		}

        #endregion
    }
}