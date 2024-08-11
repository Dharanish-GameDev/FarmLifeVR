using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : TestState
{
    public Test2(TestStateContext context, TestStateMachine.ETestState state) : base(context, state)
    {
        TestStateContext testStateContext = context;
    }

    public override void EnterState()
    {

    }
    public override void ExitState()
    {

    }
    public override void UpdateState()
    {

    }
    public override TestStateMachine.ETestState GetNextState()
    {
        return Statekey;
    }
    public override void OnTriggerEnterState(Collider other)
    {

    }
    public override void OnTriggerStayState(Collider other)
    {

    }
    public override void OnTriggerExitState(Collider other)
    {

    }
}
