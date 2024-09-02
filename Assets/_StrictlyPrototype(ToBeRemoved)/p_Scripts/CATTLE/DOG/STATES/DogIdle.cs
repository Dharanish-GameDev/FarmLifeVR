using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogIdle : DogBaseState
    {
        //Constructor
        public DogIdle(DogStateContext context, DogStateMachine.EDogState state) : base(context, state)
        {
            DogStateContext dogStateContext = context;
        }

        #region Private Variables

        #endregion


        #region Private Methods

        

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=#f4bbff> Dog Entered Idle State </color>");

            dogStateContext.DogAnimator.SetInteger(dogStateContext.DogAnimInt, 0);
        }
        public override void ExitState()
        {
            
        }
        public override void UpdateState()
        {
            
        }
        public override DogStateMachine.EDogState GetStateKey()
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

        #endregion
    }
}


