using FARMLIFEVR.STATEMACHINE;

public abstract class TestState : BaseState<TestStateMachine.ETestState>
{
	protected TestStateContext context;

	public TestState(TestStateContext context, TestStateMachine.ETestState stateKey):base(stateKey)
	{
		this.context = context;
	}
}
