using UnityEngine;

namespace FARMLIFEVR.CROPS.MAIZE
{

	public class MaizeFieldContext
	{
		#region Private Variables

		//Refs
		private MaizeFeildStateMachine maizeFieldStateMachine;


		#endregion

		//Constructor

		public MaizeFieldContext
			(
            MaizeFeildStateMachine maizeFieldStateMachine
            ) 
		{
			this.maizeFieldStateMachine = maizeFieldStateMachine;
		}

		#region Properties

		public MaizeFeildStateMachine MaizeFeildStateMachine => maizeFieldStateMachine;

		#endregion
	}
}
