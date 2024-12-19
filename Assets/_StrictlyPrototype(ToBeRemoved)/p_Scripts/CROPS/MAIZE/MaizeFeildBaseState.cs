using FARMLIFEVR.STATEMACHINE;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public abstract class MaizeFeildBaseState : BaseState<MaizeFieldStateMachine.EMaizeFieldState>
	{
        protected MaizeFieldContext maizeFieldContext;

        // Construtor
        public MaizeFeildBaseState(MaizeFieldContext maizeFieldContext, MaizeFieldStateMachine.EMaizeFieldState stateKey) : base(stateKey)
        {
            this.maizeFieldContext = maizeFieldContext;
        }

        public abstract MaizeFieldStateMachine.EMaizeFieldState GetCorrespondingNextState();

        /// <summary>
        /// Necessary Conditions to be satisfied before Switching to the Another State
        /// </summary>
        /// <returns></returns>
        public abstract bool GetHasApprovalToSwitchState();
    }
}