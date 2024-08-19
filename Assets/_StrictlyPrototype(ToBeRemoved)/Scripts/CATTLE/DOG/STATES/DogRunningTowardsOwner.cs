using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogRunningTowardsOwner : DogBaseState
    {
        public DogRunningTowardsOwner(DogStateContext context, DogStateMachine.EDogState state) : base(context, state)
        {
            DogStateContext dogStateContext = context;
        }

        #region Private Variables


        #endregion

        #region Properties



        #endregion

        #region LifeCycle Methods

        private void Awake()
        {

        }
        private void Start()
        {

        }
        private void Update()
        {

        }

        #endregion

        #region Private Methods


        #endregion

        #region Overriden Methods

        public override void EnterState()
        {

        }
        public override void ExitState()
        {

        }
        public override void UpdateState()
        {

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
