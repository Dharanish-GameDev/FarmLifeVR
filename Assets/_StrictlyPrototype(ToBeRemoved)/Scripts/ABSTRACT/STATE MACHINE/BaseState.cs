using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<EState> where EState : Enum
{
    public BaseState(EState key)
    {
        Statekey = key;
    }

    public EState Statekey;

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState();
    public abstract void OnTriggerEnterState(Collider other);
    public abstract void OnTriggerStayState(Collider other);
    public abstract void OnTriggerExitState(Collider other);

}
