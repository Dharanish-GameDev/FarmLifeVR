using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FARMLIFEVR.STATEMACHINE
{
    /// <summary>
    /// References All the State , Oversee the active state , Handle switching between the states,Calls methods of states
    /// </summary>
    /// 
    public class StateManager<EState> : MonoBehaviour where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
        public BaseState<EState> CurrentState { get; protected set; }
        protected bool isTransitioningState = false;

        private void Start()
        {
            CurrentState.EnterState();
        }

        private void Update()
        {
            EState nextStatekey = CurrentState.GetNextState();
            if (nextStatekey.Equals(CurrentState.Statekey) && !isTransitioningState)
            {
                CurrentState.UpdateState();
            }
            else if (isTransitioningState)
            {
                TransitionToState(nextStatekey);
            }
        }

        public void TransitionToState(EState statekey)
        {
            isTransitioningState = true;
            CurrentState.ExitState();
            CurrentState = States[statekey];
            CurrentState.EnterState();
            isTransitioningState = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            CurrentState.OnTriggerEnterState(other);
        }
        private void OnTriggerStay(Collider other)
        {
            CurrentState.OnTriggerStayState(other);
        }
        private void OnTriggerExit(Collider other)
        {
            CurrentState.OnTriggerExitState(other);
        }

    }

}

