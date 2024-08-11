using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestState : BaseState<TestStateMachine.ETestState>
{
	protected TestStateContext context;

	public TestState(TestStateContext context, TestStateMachine.ETestState stateKey):base(stateKey)
	{
		this.context = context;
	}
}
