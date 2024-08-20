using FARMLIFEVR.EVENTSYSTEM;
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
            Debug.Log("<color=cyan> Entered the Dog Idle State ! </color>");
        }
        public override void ExitState()
        {
            
        }
        public override void UpdateState()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("The Dog Overlap Player : " + dogStateContext.DogOwnerOverLap.GetIsOverlapping());
            }
        }
        public override DogStateMachine.EDogState GetNextState()
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


