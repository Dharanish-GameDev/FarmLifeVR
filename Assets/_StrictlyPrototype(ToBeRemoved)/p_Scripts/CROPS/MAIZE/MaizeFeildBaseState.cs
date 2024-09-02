using FARMLIFEVR.STATEMACHINE;

namespace FARMLIFEVR.CROPS.MAIZE
{
	public abstract class MaizeFeildBaseState : BaseState<MaizeFeildStateMachine.EMaizeFieldState>
	{
        protected MaizeFieldContext maizeFieldContext;

        // Construtor
        public MaizeFeildBaseState(MaizeFieldContext maizeFieldContext, MaizeFeildStateMachine.EMaizeFieldState stateKey) : base(stateKey)
        {
            this.maizeFieldContext = maizeFieldContext;
        }
    }
}