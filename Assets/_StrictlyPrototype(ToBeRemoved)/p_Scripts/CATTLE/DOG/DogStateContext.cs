using UnityEngine;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogStateContext
    {
        #region Private Variables

        // Refs
        private Animator dogAnimator;
        private DogStateMachine dogStateMachine;
        private GameObject dogFoodGameObj;

        // Values
        private float moveSpeed;
        private float rotationSpeed;

        #endregion

        #region Animation Parameters

        // Animator Params
        public readonly int DogAnimInt = Animator.StringToHash("DogAnimInt");
        public readonly int DogSitEmote = Animator.StringToHash("DogSit");
        public readonly int DogStandUpEmote = Animator.StringToHash("DogStandUp");
        public readonly int DogDieEmote = Animator.StringToHash("DogDie");
        public readonly int DogEatEmote = Animator.StringToHash("DogEat");

        #endregion


        //Constructor
        public DogStateContext
            (
            DogStateMachine dogStateMachine,
            Animator dogAnimator, 
            float moveSpeed,
            float rotationSpeed,
            GameObject dogFoodGameObj
            )
        {
            this.dogAnimator = dogAnimator;
            this.dogStateMachine = dogStateMachine;
            this.moveSpeed = moveSpeed;
            this.rotationSpeed = rotationSpeed;
            this.dogFoodGameObj = dogFoodGameObj;
        }

        #region Properties

        // Properties

        //Refs
        public Animator DogAnimator => dogAnimator;
        public DogStateMachine DogStateMachine => dogStateMachine;
        public GameObject DogFoodGameObj => dogFoodGameObj;

        //Values
        public float MoveSpeed => moveSpeed;
        public float RotationSpeed => rotationSpeed;

        #endregion

    }
}

