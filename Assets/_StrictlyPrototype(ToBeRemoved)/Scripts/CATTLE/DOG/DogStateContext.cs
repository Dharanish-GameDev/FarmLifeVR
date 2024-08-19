using UnityEngine;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogStateContext
    {

        private Animator dogAnimator;
        private DogOwnerOverLap dogOwnerOverLap;

        //Constructor
        public DogStateContext(Animator dogAnimator, DogOwnerOverLap dogOwnerOverLap)
        {
            this.dogAnimator = dogAnimator;
            this.dogOwnerOverLap = dogOwnerOverLap;
        }


        // Properties
        public Animator DogAnimator => dogAnimator;
        public DogOwnerOverLap DogOwnerOverLap => dogOwnerOverLap;
    }
}

