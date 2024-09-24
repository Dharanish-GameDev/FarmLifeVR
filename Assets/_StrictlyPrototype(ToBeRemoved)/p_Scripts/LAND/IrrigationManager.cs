using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
using System.Linq;

namespace FARMLIFEVR.LAND
{
	public class IrrigationManager : MonoBehaviour
	{
		#region Private Variables

		[SerializeField] private List<WaterCanal> waterCanals = new List<WaterCanal>();

		private Coroutine irrigationCouroutine = null;

		[SerializeField] private List<WaterCanal> evenIndexCanal = new List<WaterCanal>();
		[SerializeField] private List<WaterCanal> oddIndexCanal = new List<WaterCanal>();

		#endregion

		#region Properties

		#endregion

		#region LifeCycle Methods

		private void Awake()
		{

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
					OnEvenIndexListChanged(evenIndexCanal.IndexOf(waterCanals[i]));
				}
				else
				{
					oddIndexCanal.Add(waterCanals[i]);
				}
                yield return new WaitForSeconds(2); // The Wait Seconds Change per No of Elements in Column
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