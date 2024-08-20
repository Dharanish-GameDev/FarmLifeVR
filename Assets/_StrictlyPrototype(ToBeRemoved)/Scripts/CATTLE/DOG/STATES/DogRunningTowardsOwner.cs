using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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


        #region Private Methods


        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=cyan>Entered the Dog Running Towards State ! </color>");


            float rotationSpeed = 5f; // Rotation speed in degrees per second
            float moveSpeed = 2f; // Movement speed in units per second

            // Calculate the distance between the dog and the destination
            float distance = Vector3.Distance(dogStateContext.DogStateMachine.transform.position, GameManager.Instance.PetDestinationPoint.position);

            // Calculate the duration for movement based on the desired speed
            float moveDuration = distance / moveSpeed;

            // Rotate the dog to face the destination
            dogStateContext.DogStateMachine.transform.DOLookAt(GameManager.Instance.PetDestinationPoint.position, rotationSpeed * Time.deltaTime)
                .SetEase(Ease.Linear) // Smooth rotation
                .OnComplete(() =>
                {
                    // Move the dog towards the target position at the calculated speed
                    dogStateContext.DogStateMachine.transform.DOMove(GameManager.Instance.PetDestinationPoint.position, moveDuration)
                        .SetEase(Ease.Linear) // Smooth movement
                        .OnComplete(() => dogStateContext.DogStateMachine.SwitchState(DogStateMachine.EDogState.Idle)); // Trigger OnMoveComplete when movement is done
                });

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
