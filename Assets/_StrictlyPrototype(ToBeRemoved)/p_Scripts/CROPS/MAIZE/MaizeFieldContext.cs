using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.CROPS.MAIZE
{

	public class MaizeFieldContext
	{
		#region Private Variables

		//Refs
		private MaizeFeildStateMachine maizeFieldStateMachine;
		private HashSet<Maize> maizesHashSet;


		#endregion

		//Constructor

		public MaizeFieldContext
			(
            MaizeFeildStateMachine maizeFieldStateMachine,
			HashSet<Maize> maizesHashSet
            ) 
		{
			this.maizeFieldStateMachine = maizeFieldStateMachine;
			this.maizesHashSet = maizesHashSet;
		}

		#region Properties

		public MaizeFeildStateMachine MaizeFeildStateMachine => maizeFieldStateMachine;
		public HashSet<Maize> MaizesHashSet => maizesHashSet;

		#endregion
	}
}
