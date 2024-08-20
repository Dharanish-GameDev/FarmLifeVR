using UnityEngine;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogStateContext
    {

        private Animator dogAnimator;
        private DogOwnerOverLap dogOwnerOverLap;
        private DogStateMachine dogStateMachine;

        //Constructor
        public DogStateContext(DogStateMachine dogStateMachine,Animator dogAnimator, DogOwnerOverLap dogOwnerOverLap)
        {
            this.dogAnimator = dogAnimator;
            this.dogOwnerOverLap = dogOwnerOverLap;
            this.dogStateMachine = dogStateMachine;
        }


        // Properties
        public Animator DogAnimator => dogAnimator;
        public DogOwnerOverLap DogOwnerOverLap => dogOwnerOverLap;
        public DogStateMachine DogStateMachine => dogStateMachine;
    }
}

