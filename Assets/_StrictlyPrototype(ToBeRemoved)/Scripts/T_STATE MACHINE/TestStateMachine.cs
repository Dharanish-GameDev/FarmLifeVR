using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TestStateMachine : StateManager<TestStateMachine.ETestState>
{
	public enum ETestState
	{
		test1, test2
	}

	#region Private Variables

	private TestStateContext context;

	[Required]
	[SerializeField] private Rigidbody rigidBody;
	

	#endregion

	#region Properties



	#endregion

	#region LifeCycle Methods

	private void Awake()
	{
		ValidateConstraints();
		context = new TestStateContext(rigidBody);
		InitializeStates();
	}

    #endregion

    #region Private Methods

    private void ValidateConstraints()
    {
		Assert.IsNotNull(rigidBody,"RigidBody is Null");
    }

	private void InitializeStates() // Adds State to the inherited StateManager's Dictionary
	{
		States.Add(ETestState.test1,new Test1(context,ETestState.test1));
		States.Add(ETestState.test2, new Test2(context, ETestState.test2));
		CurrentState = States[ETestState.test1];
	}

    #endregion

    #region Public Methods


    #endregion
}
