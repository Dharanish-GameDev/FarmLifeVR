using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogEmoteState : DogBaseState
    {
        public DogEmoteState(DogStateContext context, DogStateMachine.EDogState state) : base(context, state)
        {
            DogStateContext dogStateContext = context;
        }

        #region Overriden Methods

        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }
        public override void ExitState() 
        { 
            throw new System.NotImplementedException(); 
        }
        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public override DogStateMachine.EDogState GetNextState()
        {
            throw new System.NotImplementedException();
        }
        public override void OnTriggerEnterState(Collider other)
        {
            throw new System.NotImplementedException();
        }

        public override void OnTriggerExitState(Collider other)
        {
            throw new System.NotImplementedException();
        }

        public override void OnTriggerStayState(Collider other)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}

