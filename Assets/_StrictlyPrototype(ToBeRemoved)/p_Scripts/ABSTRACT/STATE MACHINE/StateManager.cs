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

        public virtual void Awake()
        {
            ValidateConstraints();
        }

        private void Start()
        {
            CurrentState.EnterState();
        }
        private void Update()
        {
            EState nextStatekey = CurrentState.GetStateKey();
            if (nextStatekey.Equals(CurrentState.Statekey) && !isTransitioningState)
            {
                CurrentState.UpdateState();
            }
            else if (isTransitioningState)
            {
                SwitchState(nextStatekey);
            }
        }
        /// <summary>
        /// This Method Switch the State to the Given State and End the Current Existing State
        /// </summary>
        /// <param name="statekey"></param>
        public void SwitchState(EState statekey)
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

        /// <summary>
        /// It Checks All The References are Valid or not
        /// </summary>
        public virtual void ValidateConstraints() { }
        
    }

}

