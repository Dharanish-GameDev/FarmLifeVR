using FARMLIFEVR.SIMPLEINTERACTABLES;
using System.Collections.Generic;
using FARMLIFEVR.FARMTOOLS;
using UnityEngine;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public class MaizeFieldContext
	{
		#region Private Variables

		//Refs
		private MaizeFieldStateMachine maizeFieldStateMachine;
		private HashSet<Maize> maizesHashSet;
		private PipeInteractable pipeInteractable;
		private PesticideSprayerInteractable pesticideSprayerInteractable;
		private Tool_MegaphoneInteractable megaphoneInteractable;

        #endregion

        //Constructor

        public MaizeFieldContext
			(
            MaizeFieldStateMachine maizeFieldStateMachine,
			HashSet<Maize> maizesHashSet,
			PipeInteractable pipeInteractable,
			PesticideSprayerInteractable pesticideSprayerInteractable,
			Tool_MegaphoneInteractable megaphoneInteractable
            ) 
		{
			this.maizeFieldStateMachine = maizeFieldStateMachine;
			this.maizesHashSet = maizesHashSet;
			this.pipeInteractable = pipeInteractable;
			this.pesticideSprayerInteractable = pesticideSprayerInteractable;
			this.megaphoneInteractable = megaphoneInteractable;
		}

		#region Properties

		public MaizeFieldStateMachine MaizeFieldStateMachine => maizeFieldStateMachine;
		public HashSet<Maize> MaizesHashSet => maizesHashSet;
		public PipeInteractable PipeInteractable => pipeInteractable;
		public PesticideSprayerInteractable PesticideSprayerInteractable => pesticideSprayerInteractable;
		public Tool_MegaphoneInteractable MegaphoneInteractable => megaphoneInteractable;
		#endregion
	}
}
