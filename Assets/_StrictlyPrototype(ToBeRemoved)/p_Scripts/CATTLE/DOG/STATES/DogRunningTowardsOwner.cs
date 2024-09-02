using DG.Tweening;
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

        private float distance;
        private float moveDuration;
        private Vector3 targetDir;
        private float angleDiff;
        private float rotationDuration;

        #endregion


        #region Private Methods


        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            Debug.Log("<color=#f4bbff> Dog Entered Running Towards Owner State </color>");

            dogStateContext.DogAnimator.SetInteger(dogStateContext.DogAnimInt,1);

            // Calculate the distance between the dog and the destination
             distance = Vector3.Distance(dogStateContext.DogStateMachine.transform.position, GameManager.Instance.PetDestinationPoint.position);

            // Calculate the duration for movement based on the desired speed
             moveDuration = distance / dogStateContext.MoveSpeed;

            targetDir = GameManager.Instance.PetDestinationPoint.position - dogStateContext.DogStateMachine.transform.position;
            angleDiff = Vector3.Angle(dogStateContext.DogStateMachine.transform.forward, targetDir);

            rotationDuration = angleDiff / dogStateContext.RotationSpeed;

            // Rotate the dog to face the destination
            dogStateContext.DogStateMachine.transform.DOLookAt(GameManager.Instance.PetDestinationPoint.position,rotationDuration)
                .SetEase(Ease.Linear) // Smooth rotation
                .OnComplete(() =>
                {
                    dogStateContext.DogAnimator.SetInteger(dogStateContext.DogAnimInt, 2);
                    // Move the dog towards the target position at the calculated speed
                    dogStateContext.DogStateMachine.transform.DOMove(GameManager.Instance.PetDestinationPoint.position - new Vector3(0,0,1), moveDuration)
                        .SetEase(Ease.Linear) // Smooth movement
                        .OnComplete(() => 
                        {
                            dogStateContext.DogAnimator.SetInteger(dogStateContext.DogAnimInt, 1);
                            dogStateContext.DogStateMachine.transform.DOMove(GameManager.Instance.PetDestinationPoint.position, moveDuration)
                       .SetEase(Ease.Linear).OnComplete(() => dogStateContext.DogStateMachine.SwitchState(DogStateMachine.EDogState.Idle));// Smooth movement
                            
                        }); // Trigger OnMoveComplete when movement is done
                });

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
