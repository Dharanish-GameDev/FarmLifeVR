using UnityEngine;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogStateContext
    {

        private Animator dogAnimator;

        //Constructor
        public DogStateContext(Animator dogAnimator)
        {
            this.dogAnimator = dogAnimator;
        }


        // Properties
        public Animator DogAnimator => dogAnimator;
    }
}

